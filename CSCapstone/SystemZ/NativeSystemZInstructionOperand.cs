using System.Runtime.InteropServices;

namespace CSCapstone.SystemZ {
    /// <summary>
    ///     Native SystemZ Instruction Operand.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeSystemZInstructionOperand {
        /// <summary>
        ///     Operand's Type.
        /// </summary>
        public int Type;

        /// <summary>
        ///     Operand's Value.
        /// </summary>
        public NativeSystemZInstructionOperandValue Value;
    }
}