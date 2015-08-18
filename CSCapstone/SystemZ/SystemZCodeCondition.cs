using System;

namespace CSCapstone.SystemZ
{
    public enum SystemZCodeCondition
    {
        SYSZ_CC_INVALID = 0,	// invalid CC (default)

        SYSZ_CC_O,
        SYSZ_CC_H,
        SYSZ_CC_NLE,
        SYSZ_CC_L,
        SYSZ_CC_NHE,
        SYSZ_CC_LH,
        SYSZ_CC_NE,
        SYSZ_CC_E,
        SYSZ_CC_NLH,
        SYSZ_CC_HE,
        SYSZ_CC_NL,
        SYSZ_CC_LE,
        SYSZ_CC_NH,
        SYSZ_CC_NO,
    }
}
