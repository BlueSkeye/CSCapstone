using System.Runtime.InteropServices;

namespace CSCapstone.Mips {
    /// <summary>
    ///     Native ARM64 Instruction Operand.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMipsInstructionOperand {
        /// <summary>
        ///     Operand's Type.
        /// </summary>
        public int Type;

        /// <summary>
        ///     Operand's Value.
        /// </summary>
        public NativeMipsInstructionOperandValue Value;
    }
}