using System;

namespace CSCapstone.Sparc
{
    public enum SparcConditionCode
    {
        SPARC_CC_INVALID = 0,	// invalid CC (default)
        //> Integer condition codes
        SPARC_CC_ICC_A = 8 + 256,  // Always
        SPARC_CC_ICC_N = 0 + 256,  // Never
        SPARC_CC_ICC_NE = 9 + 256,  // Not Equal
        SPARC_CC_ICC_E = 1 + 256,  // Equal
        SPARC_CC_ICC_G = 10 + 256,  // Greater
        SPARC_CC_ICC_LE = 2 + 256,  // Less or Equal
        SPARC_CC_ICC_GE = 11 + 256,  // Greater or Equal
        SPARC_CC_ICC_L = 3 + 256,  // Less
        SPARC_CC_ICC_GU = 12 + 256,  // Greater Unsigned
        SPARC_CC_ICC_LEU = 4 + 256,  // Less or Equal Unsigned
        SPARC_CC_ICC_CC = 13 + 256,  // Carry Clear/Great or Equal Unsigned
        SPARC_CC_ICC_CS = 5 + 256,  // Carry Set/Less Unsigned
        SPARC_CC_ICC_POS = 14 + 256,  // Positive
        SPARC_CC_ICC_NEG = 6 + 256,  // Negative
        SPARC_CC_ICC_VC = 15 + 256,  // Overflow Clear
        SPARC_CC_ICC_VS = 7 + 256,  // Overflow Set

        //> Floating condition codes
        SPARC_CC_FCC_A = 8 + 16 + 256,  // Always
        SPARC_CC_FCC_N = 0 + 16 + 256,  // Never
        SPARC_CC_FCC_U = 7 + 16 + 256,  // Unordered
        SPARC_CC_FCC_G = 6 + 16 + 256,  // Greater
        SPARC_CC_FCC_UG = 5 + 16 + 256,  // Unordered or Greater
        SPARC_CC_FCC_L = 4 + 16 + 256,  // Less
        SPARC_CC_FCC_UL = 3 + 16 + 256,  // Unordered or Less
        SPARC_CC_FCC_LG = 2 + 16 + 256,  // Less or Greater
        SPARC_CC_FCC_NE = 1 + 16 + 256,  // Not Equal
        SPARC_CC_FCC_E = 9 + 16 + 256,  // Equal
        SPARC_CC_FCC_UE = 10 + 16 + 256,  // Unordered or Equal
        SPARC_CC_FCC_GE = 11 + 16 + 256,  // Greater or Equal
        SPARC_CC_FCC_UGE = 12 + 16 + 256,  // Unordered or Greater or Equal
        SPARC_CC_FCC_LE = 13 + 16 + 256,  // Less or Equal
        SPARC_CC_FCC_ULE = 14 + 16 + 256,  // Unordered or Less or Equal
        SPARC_CC_FCC_O = 15 + 16 + 256,  // Ordered
    }
}
