using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CSCapstone
{
    /// <summary>A base disassembler class that is templating independent.
    /// This class is intended to be further subclassed with the templated class.
    /// </summary>
    public abstract class DisassemblerBase
        : SafeCapstoneContextHandle, IDisposable
    {
        /// <summary>This class initializer is intended to collect some immutable
        /// global information from the native library.</summary>
        static DisassemblerBase()
        {
            AllArchitectureSupported = CapstoneImport.IsSupported(CapstoneImport.SpecialMode.AllArchitecture);
            DietModeEnabled = CapstoneImport.IsSupported(CapstoneImport.SpecialMode.DietMode);
            X86ReduceEnabled = CapstoneImport.IsSupported(CapstoneImport.SpecialMode.X86Reduce);
            CapstoneImport.GetLibraryVersion(out _libraryMajorVersion, out _libraryMinorVersion);
            return;
        }

        protected DisassemblerBase(SupportedArchitecture architecture, SupportedMode mode)
            : base(CreateNativeAssembler(architecture, mode))
        {
            this.Architecture = architecture;
            this._mode = mode;
            this.EnableDetails = false;
            this.Syntax = SyntaxOptionValue.Default;
            this._workInstruction = CapstoneImport.AllocateInstruction(this);
            return;
        }

        /// <summary>Get a flag that tell whether the Capstone native library has
        /// been compiled with all architectures available or not.</summary>
        public static bool AllArchitectureSupported { get; private set; }

        /// <summary>Get Disassembler's SupportedArchitecture.</summary>
        public SupportedArchitecture Architecture { get; private set; }

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
                CapstoneImport.SetOption(this, Option.Detail,
                    value
                        ? (IntPtr)OnOffOptionValue.On
                        : (IntPtr)OnOffOptionValue.Off
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
        public SupportedMode Mode {
            get {
                return this._mode;
            }
            set {
                CapstoneImport.SetOption(this, Option.Mode, (IntPtr)value)
                    .ThrowOnCapstoneError();
                this._mode = value;
            }
        }

        /// <summary>Get and Set Disassembler's Syntax.</summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassembler's syntax could not be set.
        /// </exception>
        public SyntaxOptionValue Syntax {
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

        private void AssertDetailsEnabled()
        {
            if (this.EnableDetails) { return; }
            throw new ApplicationException("Unable to complete request. Details are disabled.");
        }

        internal bool BelongsToGroup(InstructionBase candidate, uint groupId)
        {
            AssertDetailsEnabled();
            lock (_workInstruction) {
                throw new NotImplementedException();
                // return CapstoneImport.BelongsToGroup(this, TODO , groupId);
            }
        }

        /// <summary>Invoke the native Capstone exported function that will open
        /// a disassembler for the given pair of architecture and mode.</summary>
        /// <param name="architecture">Target architecture</param>
        /// <param name="mode">Target mode</param>
        /// <returns>The native handle that is to be wrapped by our super class
        /// safe handle.</returns>
        /// <remarks>This method is for use by the constructor exclusively.</remarks>
        private static IntPtr CreateNativeAssembler(SupportedArchitecture architecture,
            SupportedMode mode)
        {
            IntPtr native;
            CapstoneImport.Open(architecture, mode, out native).ThrowOnCapstoneError();
            return native;
        }

        /// <summary>Dispose Disassembler.</summary>
        /// <param name="disposing">A boolean true if the disassembler is being
        /// disposed from application code. A boolean false otherwise.</param>
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (!this._disposed) {
                if (null != _workInstruction) {
                    CapstoneImport.Free(_workInstruction, (IntPtr)1);
                    _workInstruction = null;
                }
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
            this.SetDisassembleSyntaxOption(SyntaxOptionValue.Att);
        }

        /// <summary>Enable Default Disassemble Syntax Option.</summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassemble syntax option could not be set.
        /// </exception>
        public void EnableDefaultDisassembleSyntaxOption()
        {
            this.SetDisassembleSyntaxOption(SyntaxOptionValue.Default);
        }

        /// <summary>Enable Intel Disassemble Syntax Option.</summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the disassemble syntax option could not be set.
        /// </exception>
        public void EnableIntelDisassembleSyntaxOption()
        {
            this.SetDisassembleSyntaxOption(SyntaxOptionValue.Intel);
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
        private void SetDisassembleSyntaxOption(SyntaxOptionValue value)
        {
            CapstoneImport.SetOption(this, Option.Syntax, (IntPtr)value)
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
        private SupportedMode _mode;
        /// <summary>Disassembler's Syntax.</summary>
        private SyntaxOptionValue _syntax;
        private SafeNativeInstructionHandle _workInstruction;
        
        /// <summary>Values to be used for boolean like options setting.</summary>
        public enum OnOffOptionValue
        {
            /// <summary>Turn off the Option.</summary>
            Off = 0,
            /// <summary>Turn on the Option.</summary>
            On = 3
        }
        
        /// <summary>Options that can be set on the disassembler.</summary>
        public enum Option
        {
            /// <summary>Syntax; Use <see cref="SetDisassembleSyntaxOption"/></summary>
            Syntax = 1,
            /// <summary>Enable or disable details. Use <see cref="EnableDetails"/>
            /// property.</summary>
            Detail = 2,
            /// <summary>Mode. Use <see cref="Mode"/> property.</summary>
            Mode = 3,
            /// <summary>Dynamic Memory Management. Not supported yet. It won't be in
            /// a forseeable future.</summary>
            DynamicMemoryManagement = 4,
            /// <summary>Enable or disable data skipping. Not supported yet.</summary>
            /// <remarks>TODO : add support.</remarks>
            SkipData = 5,
            /// <summary>Set data skipping behavior. Not supported yet.</summary>
            /// <remarks>TODO : add support.</remarks>
            SetupSkipData = 6
        }

        /// <summary>Capstone supported architectures. Not all of them are supported by
        /// this library. Moreover the Capstone library you use may not support all
        /// of them. Use ??? to discover which ones Capstone supports.</summary>
        public enum SupportedArchitecture
        {
            /// <summary>ARM SupportedArchitecture.</summary>
            Arm = 0,
            /// <summary>ARM-64 SupportedArchitecture.</summary>
            Arm64 = 1,
            /// <summary>MIPS SupportedArchitecture.</summary>
            Mips = 2,
            /// <summary>Intel X86 SupportedArchitecture.</summary>
            X86 = 3,
            /// <summary>PowerPC SupportedArchitecture.</summary>
            PowerPc = 4,
            /// <summary>SPARC SupportedArchitecture.</summary>
            Sparc = 5,
            /// <summary>SystemZ SupportedArchitecture.</summary>
            SystemZ = 6,
            /// <summary>XCore SupportedArchitecture.</summary>
            XCore = 7
        }
    
        /// <summary>Disassemble Mode.</summary>
        [Flags]
        public enum SupportedMode
        {
            /// <summary>Little Endian Disassemble Mode.</summary>
            LittleEndian = 0,
            /// <summary>32-Bit ARM Disassemble Mode.</summary>
            Arm32 = 0,
            /// <summary>16-Bit Disassemble Mode.</summary>
            Bit16 = 1 << 1,
            /// <summary>32-Bit Disassemble Mode.</summary>
            Bit32 = 1 << 2,
            /// <summary>64-Bit Disassemble Mode.</summary>
            Bit64 = 1 << 3,
            /// <summary>ARM Thumb Disassemble Mode.</summary>
            ArmThumb = 1 << 4,
            /// <summary>ARM Cortex-M Disassemble Mode.</summary>
            ArmCortexM = 1 << 5,
            /// <summary>ARMv8 Disassemble Mode.</summary>
            ArmV8 = 1 << 6,
            /// <summary>Micro-MIPS Disassemble Mode.</summary>
            MipsMicro = 1 << 4,
            /// <summary>MIPS-III Disassemble Mode.</summary>
            Mips3 = 1 << 5,
            /// <summary>MIPS-32R6 Disassemble Mode.</summary>
            Mips32R6 = 1 << 6,
            /// <summary>MIPS 64-Bit Wide General Purpose Disassemble Mode.</summary>
            MipsGp64 = 1 << 7,
            /// <summary>SPARCv9 Disassemble Mode.</summary>
            SparcV9 = 1 << 4,
            /// <summary>Big Endian Disassemble Mode.</summary>
            BigEndian = 1 << 31,
            /// <summary>MIPS-32 Disassemble Mode.</summary>
            Mips32 = Bit32,
            /// <summary>MIPS-64 Disassemble Mode.</summary>
            Mips64 = Bit64
        }

        /// <summary>Option values for ssyntax setting. <see cref="SetDisassembleSyntaxOption"/></summary>
        public enum SyntaxOptionValue
        {
            /// <summary>Default Syntax.</summary>
            Default = 0,
            /// <summary>Intel Syntax.</summary>
            Intel = 1,
            /// <summary>ATT Syntax.</summary>
            Att = 2,
            /// <summary>Disable Registry Name Syntax.</summary>
            DisableRegistryName = 3
        }
    }
}
