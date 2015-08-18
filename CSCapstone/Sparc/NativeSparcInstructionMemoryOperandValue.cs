using System.Runtime.InteropServices;

namespace CSCapstone.Sparc {
    /// <summary>
    ///     Native SPARC Instruction Memory Operand Value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeSparcInstructionMemoryOperandValue {
        /// <summary>
        ///     Operand Value's BaseRegister Register.
        /// </summary>
        public byte BaseRegister;

        /// <summary>
        ///     Operand Value's IndexRegister Register.
        /// </summary>
        public byte IndexRegister;

        /// <summary>
        ///     Operand Value's Displacement Value.
        /// </summary>
        public int Displacement;
    }
}