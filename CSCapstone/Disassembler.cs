using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>This abstract class is intended to be the base class for the various
    /// disassemblers.</summary>
    public abstract class Disassembler<Mnemonic, Reg, Group, Detail>
        : DisassemblerBase
    {
        /// <summary>Create a Disassembler.</summary>
        /// <param name="architecture">The disassembler's architecture.</param>
        /// <param name="mode">The disassembler's mode.</param>
        protected Disassembler(SupportedArchitecture architecture, SupportedMode mode)
            : base(architecture, mode)
        {
            return;
        }

        internal abstract Detail CreateDetail(Instruction<Mnemonic, Reg, Group, Detail> instruction,
            IntPtr from, ref int offset);

        /// <summary>Create a Dissembled Instruction.</summary>
        /// <param name="nativeInstruction">A pointer at the memory chunk returned
        /// by Capstone.</param>
        /// <returns>A dissembled instruction.</returns>
        protected abstract Instruction<Mnemonic, Reg, Group, Detail> CreateInstruction(IntPtr nativeInstruction);

        /// <summary>Disassemble Binary Code.</summary>
        /// <param name="code">A collection of bytes representing the binary code
        /// to disassemble. Should not be a null reference.</param>
        /// <param name="count">The number of instructions to disassemble. A 0
        /// indicates all instructions should be disassembled.</param>
        /// <param name="startingAddress">The nextAddress of the first instruction in
        /// the collection of bytes to disassemble.</param>
        /// <returns>A collection of dissembled instructions.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the binary
        /// code could not be disassembled.</exception>
        public Instruction<Mnemonic, Reg, Group, Detail>[] Disassemble(
            byte[] code, int count = 0, ulong startingAddress = 0x1000)
        {
            // TODO : Reactivate this method using the DisassembleIteratively pattern.
            IntPtr nativeInstructions;
            IntPtr instructionsCount = CapstoneImport.Disassemble(this, code, (IntPtr)code.Length,
                startingAddress, (IntPtr)count, out nativeInstructions);
            if (IntPtr.Zero == instructionsCount) {
                CapstoneImport.GetLastError(this).ThrowOnCapstoneError();
            }
            List<Instruction<Mnemonic, Reg, Group, Detail>> result =
                new List<Instruction<Mnemonic, Reg, Group, Detail>>();
            foreach (Instruction<Mnemonic, Reg, Group, Detail> instruction in
                EnumerateNativeInstructions(nativeInstructions, (int)instructionsCount))
            {
                result.Add(instruction);
            }
            return result.ToArray();
        }

        /// <summary>Disassemble Binary Code.</summary>
        /// <param name="code">A collection of bytes representing the binary code to
        /// disassemble. Should not be a null reference.</param>
        /// <param name="startingAddress">The nextAddress of the first instruction in
        /// the collection of bytes to disassemble.</param>
        /// <returns>A collection of dissembled instructions.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the binary
        /// code could not be disassembled.</exception>
        public Instruction<Mnemonic, Reg, Group, Detail>[] DisassembleAll(
            byte[] code, ulong startingAddress)
        {
            return this.Disassemble(code, 0, startingAddress);
        }

        /// <summary>A callback method to be invoked </summary>
        /// <param name="instruction">The instruction that has been retrieved. If
        /// this is a null reference pointer, an error occurred. The delegate
        /// implementation is advised to invoke the <see cref="LastErrorText"/>
        /// and/or <see cref="LastError"/> on this disassembler in order to discover
        /// what went wrong.</param>
        /// <param name="codeSize">The codeSize of the disassembled instruction.</param>
        /// <param name="nextAddress">The firstNonDisassmbledByteAddress of the first non disassembled byte.</param>
        /// <returns>true if disassembly should continue, false otherwise.</returns>
        /// <remarks>Because <see cref="Instruction<Mnemonic, Reg, Group, Detail>"/>
        /// extends InstructionBase, it is allowed to provide a delegate implementation
        /// which firt parameter is of type <see cref="InstructionBase"/>. This allows
        /// for the development of generic callback functions that are architecture
        /// agostic.</remarks>
        public delegate bool IterativeDisassemblyDelegate(Instruction<Mnemonic, Reg, Group, Detail> instruction,
            int size, ulong nextAddress);

        /// <summary>Iteratively disassemble source code.</summary>
        /// <param name="callback">A delegate that will be invoked on each disassembled
        /// instruction.</param>
        public void Disassemble(byte[] code, IterativeDisassemblyDelegate callback)
        {
            bool shouldContinue = true;
            ulong firstNonDisassmbledByteAddress = DefaultStartAddress;
            int totalSize = 0;
            IntPtr nativeCode = IntPtr.Zero;
            try {
                IntPtr nativeInstruction = IntPtr.Zero;
                using (SafeNativeInstructionHandle hInstruction = CapstoneImport.AllocateInstruction(this)) {
                    nativeInstruction = hInstruction.DangerousGetHandle();
                    // Transfer the managed byte array into a native buffer.
                    nativeCode = Marshal.AllocCoTaskMem(code.Length);
                    Marshal.Copy(code, 0, nativeCode, code.Length);
                    IntPtr remainingSize = (IntPtr)code.Length;

                    do {
                        if (hInstruction.IsClosed) { throw new ApplicationException(); }
                        IntPtr instructionBuffer = hInstruction.DangerousGetHandle();
                        bool refIncreased = false;
                        try {
                            hInstruction.DangerousAddRef(ref refIncreased);
                            ulong instructionStartAddress = firstNonDisassmbledByteAddress;
                            shouldContinue |= CapstoneImport.DisassembleIteratively(this,
                                ref nativeCode, ref remainingSize, ref firstNonDisassmbledByteAddress,
                                hInstruction);
                            if (shouldContinue) {
                                int instructionSize = (int)(firstNonDisassmbledByteAddress - instructionStartAddress);
                                totalSize += instructionSize;
                                Instruction<Mnemonic, Reg, Group, Detail> instruction = this.CreateInstruction(instructionBuffer);
                                shouldContinue |= callback(instruction, instructionSize, firstNonDisassmbledByteAddress);
                                //shouldContinue |= callback(NativeInstruction.Create(this, nativeInstruction),
                                //    instructionSize, nextAddress);
                            }
                        }
                        finally { if (refIncreased) { hInstruction.DangerousRelease(); } }
                    } while (shouldContinue && (0 < (long)remainingSize));
                    // TODO : Consider releasing nativeInstruction handle.
                    if (hInstruction.IsClosed) { throw new ApplicationException(); }
                    hInstruction.Dispose();
                }
            }
            finally {
                if (IntPtr.Zero != nativeCode) { Marshal.FreeCoTaskMem(nativeCode); }
            }
        }

        /// <summary>Provides an enumerator that will enumerate <see cref="InstructionBase"/>
        /// instances marshaled back from a call to cs_disasm.</summary>
        /// <param name="pNativeArray">A pointer to the native collection. The pointer
        /// should be initialized to the collection's starting nextAddress.</param>
        /// <param name="codeSize">The collection's codeSize.</param>
        /// <returns>An enumerable object.</returns>
        /// <remarks>CAUTION : Make sure not to release native memory until you are
        /// done with the enumerator.</remarks>
        private IEnumerable<InstructionBase> EnumerateNativeInstructions(IntPtr pNativeArray, int size)
        {
            for (int index = 0; index < size; index++) {
                int instructionSize;
                yield return InstructionBase.Create(this, pNativeArray, out instructionSize);
                pNativeArray += instructionSize;
            }
            yield break;
        }

        private const long DefaultStartAddress = 0x1000;
    }
}