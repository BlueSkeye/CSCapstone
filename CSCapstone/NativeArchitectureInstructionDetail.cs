using CSCapstone.Arm;
using CSCapstone.Arm64;
using CSCapstone.Mips;
using CSCapstone.PowerPc;
using CSCapstone.Sparc;
using CSCapstone.SystemZ;
using CSCapstone.X86;
using CSCapstone.XCore;
using System;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>
    ///     Native Architecture Dependent Instruction Detail.
    /// </summary>
    [Obsolete("Deprecated.")]
    [StructLayout(LayoutKind.Explicit)]
    public struct NativeArchitectureInstructionDetail {
        /// <summary>
        ///     Instruction's X86 Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativeX86InstructionDetail X86Detail;

        /// <summary>
        ///     Instruction's ARM64 Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativeArm64InstructionDetail Arm64Detail;

        // <summary>
        ///     Instruction's ARM64 Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativeArmInstructionDetail ArmDetail;

        /// <summary>
        ///     Instruction's MIPS Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativeMipsInstructionDetail MipsDetail;

        /// <summary>
        ///     Instruction's PowerPC Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativePowerPcInstructionDetail PowerPcDetail;

        /// <summary>
        ///     Instruction's SPARC Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativeSparcInstructionDetail SparcDetail;

        /// <summary>
        ///     Instruction's SystemZ Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativeSystemZInstructionDetail SystemZDetail;

        /// <summary>
        ///     Instruction's XCore Architecture Detail.
        /// </summary>
        [FieldOffset(0)] public NativeXCoreInstructionDetail XCoreDetail;
    }
}