using System.Runtime.InteropServices;

namespace CSCapstone.XCore {
    /// <summary>
    ///     Native X86 Instruction Memory Operand Value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeXCoreInstructionMemoryOperandValue {
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

        /// <summary>
        ///     Operand Value's Displacement Value.
        /// </summary>
        public int Direct;
    }
}