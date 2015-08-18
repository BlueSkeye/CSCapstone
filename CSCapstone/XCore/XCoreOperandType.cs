using System;

namespace CSCapstone.XCore
{
    public enum XCoreOperandType
    {
        XCORE_OP_INVALID = 0, // = CS_OP_INVALID (Uninitialized).
        XCORE_OP_REG, // = CS_OP_REG (Register operand).
        XCORE_OP_IMM, // = CS_OP_IMM (Immediate operand).
        XCORE_OP_MEM, // = CS_OP_MEM (Memory operand).
    }
}
