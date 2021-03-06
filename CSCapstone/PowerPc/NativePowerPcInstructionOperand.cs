﻿using System.Runtime.InteropServices;

namespace CSCapstone.PowerPc {
    /// <summary>
    ///     Native PowerPC Instruction Operand.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativePowerPcInstructionOperand {
        /// <summary>
        ///     Operand's Type.
        /// </summary>
        public int Type;

        /// <summary>
        ///     Operand's Value.
        /// </summary>
        public NativePowerPcInstructionOperandValue Value;
    }
}