// ReSharper disable InconsistentNaming

namespace CSCapstone.Arm64
{
    /// <summary>ARM64 Instruction Group.</summary>
    public enum Arm64InstructionGroup
    {
        Invalid = 0,

        JUMP = IndependentInstructionGroup.JUMP,

        CRYPTO = 128,
        FPARMV8,
        NEON,
        CRC
    }
}