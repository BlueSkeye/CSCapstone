using System;

namespace CSCapstone.XCore
{
    public enum XCoreRegister
    {
        XCORE_REG_INVALID = 0,

        XCORE_REG_CP,
        XCORE_REG_DP,
        XCORE_REG_LR,
        XCORE_REG_SP,
        XCORE_REG_R0,
        XCORE_REG_R1,
        XCORE_REG_R2,
        XCORE_REG_R3,
        XCORE_REG_R4,
        XCORE_REG_R5,
        XCORE_REG_R6,
        XCORE_REG_R7,
        XCORE_REG_R8,
        XCORE_REG_R9,
        XCORE_REG_R10,
        XCORE_REG_R11,

        //> pseudo registers
        XCORE_REG_PC,	// pc

        // internal thread registers
        // see The-XMOS-XS1-Architecture(X7879A).pdf
        XCORE_REG_SCP,	// save pc
        XCORE_REG_SSR,	// save status
        XCORE_REG_ET,	// exception type
        XCORE_REG_ED,	// exception data
        XCORE_REG_SED,	// save exception data
        XCORE_REG_KEP,	// kernel entry pointer
        XCORE_REG_KSP,	// kernel stack pointer
        XCORE_REG_ID,	// thread ID
    }
}
