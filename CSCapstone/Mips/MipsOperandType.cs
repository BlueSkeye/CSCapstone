using System;

namespace CSCapstone.Mips
{
    public enum MipsOperandType
    {
	    MIPS_OP_INVALID = 0, // = CS_OP_INVALID (Uninitialized).
	    MIPS_OP_REG, // = CS_OP_REG (Register operand).
	    MIPS_OP_IMM, // = CS_OP_IMM (Immediate operand).
	    MIPS_OP_MEM, // = CS_OP_MEM (Memory operand).
    }
}
