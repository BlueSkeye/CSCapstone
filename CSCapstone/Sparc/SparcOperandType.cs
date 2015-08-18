using System;

namespace CSCapstone.Sparc
{
    public enum SparcOperandType
    {
        SPARC_OP_INVALID = 0, // = CS_OP_INVALID (Uninitialized).
        SPARC_OP_REG, // = CS_OP_REG (Register operand).
        SPARC_OP_IMM, // = CS_OP_IMM (Immediate operand).
        SPARC_OP_MEM, // = CS_OP_MEM (Memory operand).
    }
}
