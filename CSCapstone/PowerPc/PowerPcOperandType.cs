using System;

namespace CSCapstone.PowerPc
{
    public enum PowerPcOperandType
    {
        PPC_OP_INVALID = 0, // = CS_OP_INVALID (Uninitialized).
        PPC_OP_REG, // = CS_OP_REG (Register operand).
        PPC_OP_IMM, // = CS_OP_IMM (Immediate operand).
        PPC_OP_MEM, // = CS_OP_MEM (Memory operand).
        PPC_OP_CRX = 64,	// Condition Register field
    }
}
