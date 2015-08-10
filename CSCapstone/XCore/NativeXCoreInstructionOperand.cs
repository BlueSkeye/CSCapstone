using System.Runtime.InteropServices;

namespace CSCapstone.XCore {
    /// <summary>
    ///     Native XCore Instruction Operand.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeXCoreInstructionOperand {
        /// <summary>
        ///     Operand's Type.
        /// </summary>
        public int Type;

        /// <summary>
        ///     Operand's Value.
        /// </summary>
        public NativeXCoreInstructionOperandValue Value;
    }
}