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

        /// <summary>Get Disassembler's Architecture.</summary>
        public DisassembleArchitecture Architecture { get; private set; }

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
        /// <param name="size">The collection's size.</param>
        /// <returns>An enumerable object.</returns>
        /// <remarks>CAUTION : Make sure not to release native memory until you are
        /// done with the enumerator.</remarks>
        private static IEnumerable<NativeInstruction> EnumerateNativeInstructions(IntPtr pNativeArray, int size)
        {
            for (int index = 0; index < size; index++) {
                yield return NativeInstruction.Create(pNativeArray);
                pNativeArray += Marshal.SizeOf(typeof(NativeInstruction));
            }
            yield break;
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

        private const long DefaultStatsAddress = 0x1000;
        /// <summary>Disassembler's Details Flag.</summary>
        private bool _detailsFlag;
        /// <summary>Disposed Flag.</summary>
        private bool _disposed;
        /// <summary>Disassembler's Mode.</summary>
        private DisassembleMode _mode;
        /// <summary>Disassembler's Syntax.</summary>
        private DisassembleSyntaxOptionValue _syntax;
    }
}