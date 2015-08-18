using System;

namespace CSCapstone.SystemZ
{
    [Flags()]
    public enum SystemZOperandType
    {
        SYSZ_OP_INVALID = 0, // = CS_OP_INVALID (Uninitialized).
        SYSZ_OP_REG, // = CS_OP_REG (Register operand).
        SYSZ_OP_IMM, // = CS_OP_IMM (Immediate operand).
        SYSZ_OP_MEM, // = CS_OP_MEM (Memory operand).
        
        SYSZ_OP_ACREG = 64,	// Access register operand.
    }
}
