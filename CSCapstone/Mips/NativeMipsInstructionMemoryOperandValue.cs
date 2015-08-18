using System.Runtime.InteropServices;

namespace CSCapstone.Mips {
    /// <summary>
    ///     Native MIPS Instruction Memory Operand Value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMipsInstructionMemoryOperandValue {
        /// <summary>
        ///     Operand Value's BaseRegister Register.
        /// </summary>
        public uint BaseRegister;

        /// <summary>
        ///     Operand Value's Displacement Value.
        /// </summary>
        public long Displacement;
    }
}