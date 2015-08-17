namespace CSCapstone.X86 {
    /// <summary>X86 Instruction Operand Type.</summary>
    public enum X86OperandType {
        /// <summary>Invalid Operand Type.</summary>
        Invalid = 0,

        Register,
        Immediate,
        Memory,
        FloatingPoint
    }
}