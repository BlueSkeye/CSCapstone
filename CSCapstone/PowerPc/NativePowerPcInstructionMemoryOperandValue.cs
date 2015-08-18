using System.Runtime.InteropServices;

namespace CSCapstone.PowerPc {
    /// <summary>
    ///     Native ARM64 Instruction Memory Operand Value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativePowerPcInstructionMemoryOperandValue {
        /// <summary>
        ///     Operand Value's BaseRegister Register.
        /// </summary>
        public uint BaseRegister;

        /// <summary>
        ///     Operand Value's Displacement Value.
        /// </summary>
        public int Displacement;
    }
}