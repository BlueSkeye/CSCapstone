 // ReSharper disable InconsistentNaming

namespace CSCapstone.Arm64 {
    /// <summary>
    ///     ARM64 Instruction Operand Shifter Type.
    /// </summary>
    public enum Arm64ShifterType {
        /// <summary>
        ///     Invalid Operand Shifter Type.
        /// </summary>
        Invalid = 0,

        LSL = 1,
        MSL = 2,
        LSR = 3,
        ASR = 4,
        ROR = 5
    }
}