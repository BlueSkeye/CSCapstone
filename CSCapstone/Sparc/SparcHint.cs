using System;

namespace CSCapstone.Sparc
{
    public enum SparcHint
    {
        SPARC_HINT_INVALID = 0,	// no hint
        SPARC_HINT_A = 1 << 0,	// annul delay slot instruction
        SPARC_HINT_PT = 1 << 1,	// branch taken
        SPARC_HINT_PN = 1 << 2,	// branch NOT taken
    }
}
