using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace CSCapstone.Arm {
    /// <summary>
    ///     ARM SETEND Instruction Operand Type.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ArmSetEndInstructionOperandType {
        /// <summary>
        ///     Invalid Operand Type.
        /// </summary>
        Invalid = 0,

        BE,
        LE
    }
}