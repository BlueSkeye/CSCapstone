using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using CSCapstone.Arm;
using CSCapstone.Arm64;
using CSCapstone.X86;

namespace CSCapstone {
    /// <summary>This abstract class is intended to be the base class for the various
    /// disassemblers.</summary>
    public abstract class CapstoneDisassembler<Inst, Reg, Group, Detail> 
        : SafeCapstoneContextHandle, IDisposable
    {
        /// <summary>This class initializer is intended to collect some immutable
        /// global information from the native library.</summary>
        static CapstoneDisassembler()
        {
            AllArchitectureSupported = CapstoneImport.IsSupported(CapstoneImport.SpecialMode.AllArchitecture);
            DietModeEnabled = CapstoneImport.IsSupported(CapstoneImport.SpecialMode.DietMode);
            X86ReduceEnabled = CapstoneImport.IsSupported(CapstoneImport.SpecialMode.X86Reduce);
            CapstoneImport.GetLibraryVersion(out _libraryMajorVersion, out _libraryMinorVersion);
            return;
        }

        /// <summary>Create a Disassembler.</summary>
        /// <param name="architecture">The disassembler's architecture.</param>
        /// <param name="mode">The disassembler's mode.</param>
        protected CapstoneDisassembler(DisassembleArchitecture architecture, DisassembleMode mode)
            : base(CreateNativeAssembler(architecture, mode))
        {
            this.Architecture = architecture;
            this._mode = mode;
            this.EnableDetails = false;
            this.Syntax = DisassembleSyntaxOptionValue.Default;
            return;
        }

        /// <summary>Get a flag that tell whether the Capstone native library has
        /// been compiled with all architectures available or not.</summary>
        public static bool AllArchitectureSupported { get; private set; }

        /// <summary>Get Disassembler's Architecture.</summary>
        public DisassembleArchitecture Architecture { get; private set; }

        /// <summary>Get a flag that tell whether the Capstone native library has
        /// been compiled with DIET_MODE or not.</summary>
        public static bool DietModeEnabled { get; private set; }

        /// <summary>
        ///     Enable or Disable Disassemble Details.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the disassemble details could not be set.
        /// </exception>
        public bool EnableDetails {
            get {
                return this._detailsFlag;
            }
            set {
                CapstoneImport.SetOption(this, DisassembleOptionType.Detail,
                    value
                        ? (IntPtr)DisassembleOptionValue.On
                        : (IntPtr)DisassembleOptionValue.Off
                    )
                    .ThrowOnCapstoneError();
                this._detailsFlag = value;
            }
        }

        /// <summary>Retrieve last error that occured on this disassembler.</summary>
        public CapstoneErrorCode LastError
        {
            get { return CapstoneImport.GetLastError(this); }
        }

        /// <summary>Retrieve a text representation of the last errror that
        /// occurred on this disassembler.</summary>
        public string LastErrorText
        {
            get { return CapstoneImport.GetErrorText(CapstoneImport.GetLastError(this)); }
        }

        public static int LibraryMajorVersion
        {
            get { return _libraryMajorVersion; }
        }

        public static int LibraryMinorVersion
        {
            get { return _libraryMinorVersion; }
        }

        /// <summary>Get and Set Disassembler's Mode.</summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassembler's mode could not be set.
        /// </exception>
        public DisassembleMode Mode {
            get {
                return this._mode;
            }
            set {
                CapstoneImport.SetOption(this, DisassembleOptionType.Mode, (IntPtr)value)
                    .ThrowOnCapstoneError();
                this._mode = value;
            }
        }

        /// <summary>Get and Set Disassembler's Syntax.</summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassembler's syntax could not be set.
        /// </exception>
        public DisassembleSyntaxOptionValue Syntax {
            get {
                return this._syntax;
            }
            set {
                this.SetDisassembleSyntaxOption(value);
                this._syntax = value;
            }
        }

        /// <summary>Get a flag that tell whether the Capstone native library has
        /// been compiled with CAPSTONE_X86_REDUCE or not.</summary>
        public static bool X86ReduceEnabled { get; private set; }

        /// <summary>Invoke the native Capstone exported function that will open
        /// a disassembler for the given pair of architecture and mode.</summary>
        /// <param name="architecture">Target architecture</param>
        /// <param name="mode">Target mode</param>
        /// <returns>The native handle that is to be wrapped by our super class
        /// safe handle.</returns>
        /// <remarks>This method is for use by the constructor exclusively.</remarks>
        private static IntPtr CreateNativeAssembler(DisassembleArchitecture architecture,
            DisassembleMode mode)
        {
            IntPtr native;
            CapstoneImport.Open(architecture, mode, out native).ThrowOnCapstoneError();
            return native;
        }

        /// <summary>Create a Dissembled Instruction.</summary>
        /// <param name="nativeInstruction">A native instruction.</param>
        /// <returns>A dissembled instruction.</returns>
        protected abstract Instruction<Inst, Reg, Group, Detail> CreateInstruction(NativeInstruction nativeInstruction);

        /// <summary>Disassemble Binary Code.</summary>
        /// <param name="code">A collection of bytes representing the binary code
        /// to disassemble. Should not be a null reference.</param>
        /// <param name="count">The number of instructions to disassemble. A 0
        /// indicates all instructions should be disassembled.</param>
        /// <param name="startingAddress">The address of the first instruction in
        /// the collection of bytes to disassemble.</param>
        /// <returns>A collection of dissembled instructions.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the binary
        /// code could not be disassembled.</exception>
        public Instruction<Inst, Reg, Group, Detail>[] Disassemble(
            byte[] code, int count = 0, ulong startingAddress = 0x1000)
        {
            IntPtr nativeInstructions;
            IntPtr instructionsCount = CapstoneImport.Disassemble(this, code, (IntPtr)code.Length,
                startingAddress, (IntPtr)count, out nativeInstructions);
            if (IntPtr.Zero == instructionsCount) {
                CapstoneImport.GetLastError(this).ThrowOnCapstoneError();
            }
            return EnumerateNativeInstructions(nativeInstructions, (int)instructionsCount)
                .Select(this.CreateInstruction)
                .ToArray();
        }

        /// <summary>A callback method to be invoked </summary>
        /// <param name="instruction">The instruction that has been retrieved. If
        /// this is a null reference pointer, an error occurred. The delegate
        /// implementation is advised to invoke the <see cref="LastErrorText"/>
        /// and/or <see cref="LastError"/> on this disassembler in order to discover
        /// what went wrong.</param>
        /// <param name="codeSize">The codeSize of the disassembled instruction.</param>
        /// <param name="address">The address of the disassembled instruction.</param>
        /// <returns>true if disassembly should continue, false otherwise.</returns>
        public delegate bool IterativeDisassemblyDelegate(NativeInstruction instruction,
            int size, ulong address);

        /// <summary>Iteratively disassemble source code.</summary>
        /// <param name="callback">A delegate that will be invoked on each disassembled
        /// instruction.</param>
        public void Disassemble(byte[] code, IterativeDisassemblyDelegate callback)
        {
            bool shouldContinue = true;
            ulong address = DefaultStartAddress;
            int totalSize = 0;
            IntPtr nativeCode = IntPtr.Zero;
            try {
                using (SafeNativeInstructionHandle hInstruction = CapstoneImport.AllocateInstruction(this)) {
                    IntPtr nativeInstruction = hInstruction.DangerousGetHandle();
                    // Transfer the managed byte array into a native buffer.
                    nativeCode = Marshal.AllocCoTaskMem(code.Length);
                    Marshal.Copy(code, 0, nativeCode, code.Length);
                    IntPtr remainingSize = (IntPtr)code.Length;

                    do {
                        ulong instructionStartAddress = address;
                        shouldContinue |= CapstoneImport.DisassembleIteratively(this,
                            ref nativeCode, ref remainingSize, ref address, hInstruction);
                        if (shouldContinue) {
                            int instructionSize = (int)(address - instructionStartAddress);
                            totalSize += instructionSize;
                            shouldContinue |= callback(NativeInstruction.Create(this, nativeInstruction),
                                instructionSize, address);
                        }
                    } while (shouldContinue && (0 < (long)remainingSize));
                    // TODO : Consider releasing nativeInstruction handle.
                }
            }
            finally {
                if (IntPtr.Zero != nativeCode) { Marshal.FreeCoTaskMem(nativeCode); }
            }
        }

        /// <summary>Disassemble Binary Code.</summary>
        /// <param name="code">A collection of bytes representing the binary code to
        /// disassemble. Should not be a null reference.</param>
        /// <param name="startingAddress">The address of the first instruction in
        /// the collection of bytes to disassemble.</param>
        /// <returns>A collection of dissembled instructions.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the binary
        /// code could not be disassembled.</exception>
        public Instruction<Inst, Reg, Group, Detail>[] DisassembleAll(
            byte[] code, ulong startingAddress)
        {
            return this.Disassemble(code, 0, startingAddress);
        }

        /// <summary>Dispose Disassembler.</summary>
        /// <param name="disposing">A boolean true if the disassembler is being
        /// disposed from application code. A boolean false otherwise.</param>
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (!this._disposed) {
                GC.SuppressFinalize(this);
                this._disposed = true;
            }
        }

        /// <summary>Enable ATT Disassemble Syntax Option.</summary>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the disassemble syntax option could not be set.
        /// </exception>
        public void EnableAttDisassembleSyntaxOption()
        {
            this.SetDisassembleSyntaxOption(DisassembleSyntaxOptionValue.Att);
        }

        /// <summary>Enable Default Disassemble Syntax Option.</summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassemble syntax option could not be set.
        /// </exception>
        public void EnableDefaultDisassembleSyntaxOption()
        {
            this.SetDisassembleSyntaxOption(DisassembleSyntaxOptionValue.Default);
        }

        /// <summary>Enable Intel Disassemble Syntax Option.</summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassemble syntax option could not be set.
        /// </exception>
        public void EnableIntelDisassembleSyntaxOption()
        {
            this.SetDisassembleSyntaxOption(DisassembleSyntaxOptionValue.Intel);
        }

        /// <summary>Provides an enumerator that will enumerate <see cref="NativeInstruction"/>
        /// instances marshaled back from a call to cs_disasm.</summary>
        /// <param name="pNativeArray">A pointer to the native collection. The pointer
        /// should be initialized to the collection's starting address.</param>
        /// <param name="codeSize">The collection's codeSize.</param>
        /// <returns>An enumerable object.</returns>
        /// <remarks>CAUTION : Make sure not to release native memory until you are
        /// done with the enumerator.</remarks>
        private IEnumerable<NativeInstruction> EnumerateNativeInstructions(IntPtr pNativeArray, int size)
        {
            for (int index = 0; index < size; index++) {
                yield return NativeInstruction.Create(this, pNativeArray);
                pNativeArray += Marshal.SizeOf(typeof(NativeInstruction));
            }
            yield break;
        }

        /// <summary>Return friendly name of a group id (that an instruction can
        /// belong to) Find the group id from header file of corresponding
        /// architecture (arm.h for ARM, x86.h for X86, ...)</summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <remarks>Attempt to invoke this method with Capstone native library
        /// compiled in diet mode will throw an exception.</remarks>
        public string GetGroupName(uint groupId)
        {
            if (DietModeEnabled) {
                throw new InvalidOperationException("Diet mode enabled.");
            }
            return CapstoneImport.GroupName(this, groupId);
        }

        /// <summary>Resolve an Instruction Unique Identifier to an Instruction Name.
        /// </summary>
        /// <returns>A string representing instruction name of a null reference if
        /// the unique identifier is invalid.</returns>
        public string GetInstructionName(uint instructionId)
        {
            return CapstoneImport.GroupName(this, instructionId);
        }

        /// <summary>Resolve a Registry Unique Identifier to an Registry Name.
        /// </summary>
        /// <returns>A string representing registry name of a null reference if
        /// the unique identifier is invalid.</returns>
        public string GetRegistryName(uint registryId)
        {
            return CapstoneImport.GroupName(this, registryId);
        }

        /// <summary>Set Disassemble Syntax Option.</summary>
        /// <param name="value">A syntax option value.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassemble syntax option could not be set.
        /// </exception>
        private void SetDisassembleSyntaxOption(DisassembleSyntaxOptionValue value)
        {
            CapstoneImport.SetOption(this, DisassembleOptionType.Syntax, (IntPtr)value)
                .ThrowOnCapstoneError();
        }

        private const long DefaultStartAddress = 0x1000;
        /// <summary>Disassembler's Details Flag.</summary>
        private bool _detailsFlag;
        /// <summary>Disposed Flag.</summary>
        private bool _disposed;
        private static readonly int _libraryMajorVersion;
        private static readonly int _libraryMinorVersion;
        /// <summary>Disassembler's Mode.</summary>
        private DisassembleMode _mode;
        /// <summary>Disassembler's Syntax.</summary>
        private DisassembleSyntaxOptionValue _syntax;
    }
}