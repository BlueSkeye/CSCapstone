using System.Runtime.InteropServices;

namespace CSCapstone.Sparc {
    /// <summary>
    ///     Native SPARC Instruction Operand.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeSparcInstructionOperand {
        /// <summary>
        ///     Operand's Type.
        /// </summary>
        public int Type;

        /// <summary>
        ///     Operand's Value.
        /// </summary>
        public NativeSparcInstructionOperandValue Value;
    }
}