using CSCapstone.Arm;
using CSCapstone.Arm64;
using System;
using System.Linq;
using CSCapstone.X86;

namespace CSCapstone {
    /// <summary>This abstract class is intended to be the base class for the various
    /// disassemblers.</summary>
    public abstract class CapstoneDisassembler : SafeCapstoneContextHandle, IDisposable
    {
        /// <summary>
        ///     Create a Disassembler.
        /// </summary>
        /// <param name="architecture">
        ///     The disassembler's architecture.
        /// </param>
        /// <param name="mode">
        ///     The disassembler's mode.
        /// </param>
        protected CapstoneDisassembler(DisassembleArchitecture architecture, DisassembleMode mode)
            : base(CreateNativeAssembler(architecture, mode))
        {
            this._architecture = architecture;
            this._mode = mode;
            this.EnableDetails = false;
            this.Syntax = DisassembleSyntaxOptionValue.Default;
            return;
        }

        /// <summary>
        ///     Disassembler's Architecture.
        /// </summary>
        private readonly DisassembleArchitecture _architecture;

        /// <summary>
        ///     Disassembler's Details Flag.
        /// </summary>
        private bool _detailsFlag;

        /// <summary>
        ///     Disposed Flag.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Disassembler's Mode.
        /// </summary>
        private DisassembleMode _mode;

        /// <summary>
        ///     Disassembler's Syntax.
        /// </summary>
        private DisassembleSyntaxOptionValue _syntax;

        /// <summary>
        ///     Get Disassembler's Architecture.
        /// </summary>
        public DisassembleArchitecture Architecture {
            get {
                return this._architecture;
            }
        }

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

        /// <summary>Get Disassembler's Handle.</summary>
        protected SafeCapstoneHandle Handle {
            get {
                return this;
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

        /// <summary>
        ///     Create an ARM Disassembler.
        /// </summary>
        /// <param name="mode">
        ///     The disassembler's mode.
        /// </param>
        /// <returns>
        ///     A capstone disassembler.
        /// </returns>
        public static CapstoneDisassembler<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail> CreateArmDisassembler(DisassembleMode mode) {
            var @object = new CapstoneArmDisassembler(mode);
            return @object;
        }

        /// <summary>
        ///     Create a ARM64 Disassembler.
        /// </summary>
        /// <param name="mode">
        ///     The disassembler's mode.
        /// </param>
        /// <returns>
        ///     A capstone disassembler.
        /// </returns>
        public static CapstoneDisassembler<Arm64Instruction, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail> CreateArm64Disassembler(DisassembleMode mode) {
            var @object = new CapstoneArm64Disassembler(mode);
            return @object;
        }

        /// <summary>
        ///     Create an X86 Disassembler.
        /// </summary>
        /// <param name="mode">
        ///     The disassembler's mode.
        /// </param>
        /// <returns>
        ///     A capstone disassembler.
        /// </returns>
        public static CapstoneDisassembler<X86Instruction, X86Register, X86InstructionGroup, X86InstructionDetail> CreateX86Disassembler(DisassembleMode mode) {
            var @object = new CapstoneX86Disassembler(mode);
            return @object;
        }

        /// <summary>
        ///     Dispose Disassembler.
        /// </summary>
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Dispose Disassembler.
        /// </summary>
        /// <param name="disposing">
        ///     A boolean true if the disassembler is being disposed from application code. A boolean false otherwise.
        /// </param>
        protected virtual void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (!this._disposed) {
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
    }

    /// <summary>
    ///     Capstone Disassembler.
    /// </summary>
    public abstract class CapstoneDisassembler<TArchitectureInstruction, TArchitectureRegister, TArchitectureGroup, TArchitectureDetail> : CapstoneDisassembler {
        /// <summary>
        ///     Create a Disassembler.
        /// </summary>
        /// <param name="architecture">
        ///     The disassembler's architecture.
        /// </param>
        /// <param name="mode">
        ///     The disassembler's mode.
        /// </param>
        protected CapstoneDisassembler(DisassembleArchitecture architecture, DisassembleMode mode) : base(architecture, mode) {}

        /// <summary>
        ///     Disassemble Binary Code.
        /// </summary>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <param name="count">
        ///     The number of instructions to disassemble. A 0 indicates all instructions should be disassembled.
        /// </param>
        /// <param name="startingAddress">
        ///     The address of the first instruction in the collection of bytes to disassemble.
        /// </param>
        /// <returns>
        ///     A collection of dissembled instructions.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public Instruction<TArchitectureInstruction, TArchitectureRegister, TArchitectureGroup, TArchitectureDetail>[] Disassemble(byte[] code, int count, long startingAddress) {
            var nativeInstructions = NativeCapstone.Disassemble(this.Handle, code, count, startingAddress);
            var instructions = nativeInstructions
                .Instructions
                .Select(this.CreateInstruction)
                .ToArray();

            return instructions;
        }

        /// <summary>
        ///     Disassemble Binary Code.
        /// </summary>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <param name="count">
        ///     The number of instructions to disassemble. A 0 indicates all instructions should be disassembled.
        /// </param>
        /// <returns>
        ///     A collection of dissembled instructions.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public Instruction<TArchitectureInstruction, TArchitectureRegister, TArchitectureGroup, TArchitectureDetail>[] Disassemble(byte[] code, int count) {
            var instructions = this.Disassemble(code, count, 0x1000);
            return instructions;
        }

        /// <summary>
        ///     Disassemble Binary Code.
        /// </summary>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <param name="startingAddress">
        ///     The address of the first instruction in the collection of bytes to disassemble.
        /// </param>
        /// <returns>
        ///     A collection of dissembled instructions.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public Instruction<TArchitectureInstruction, TArchitectureRegister, TArchitectureGroup, TArchitectureDetail>[] DisassembleAll(byte[] code, long startingAddress) {
            var instructions = this.Disassemble(code, 0, startingAddress);
            return instructions;
        }

        /// <summary>
        ///     Disassemble Binary Code.
        /// </summary>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <returns>
        ///     A collection of dissembled instructions.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public Instruction<TArchitectureInstruction, TArchitectureRegister, TArchitectureGroup, TArchitectureDetail>[] DisassembleAll(byte[] code) {
            var instructions = this.DisassembleAll(code, 0x1000);
            return instructions;
        }

        /// <summary>
        ///     Create a Dissembled Instruction.
        /// </summary>
        /// <param name="nativeInstruction">
        ///     A native instruction.
        /// </param>
        /// <returns>
        ///     A dissembled instruction.
        /// </returns>
        protected abstract Instruction<TArchitectureInstruction, TArchitectureRegister, TArchitectureGroup, TArchitectureDetail> CreateInstruction(NativeInstruction nativeInstruction);
    }
}