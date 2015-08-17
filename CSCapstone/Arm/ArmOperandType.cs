﻿#pragma warning disable 1591

namespace CSCapstone.Arm {
    /// <summary>
    ///     ARM Instruction Operand Type.
    /// </summary>
    public enum ArmOperandType {
        /// <summary>
        ///     Invalid Instruction Operand Type.
        /// </summary>
        Invalid = 0,

        Register,
        Immediate,
        Memory,
        FloatingPoint,
        CImmediate = 64,
        PImmediate,
        SetEnd,
        SysRegister
    }
}