using System;

namespace CSCapstone.Sparc
{
    public enum SparcInstructionGroup
    {
        SPARC_GRP_INVALID = 0, // = CS_GRP_INVALID

        //> Generic groups
        // all jump instructions (conditional+direct+indirect jumps)
        SPARC_GRP_JUMP,	// = CS_GRP_JUMP

        //> Architecture-specific groups
        SPARC_GRP_HARDQUAD = 128,
        SPARC_GRP_V9,
        SPARC_GRP_VIS,
        SPARC_GRP_VIS2,
        SPARC_GRP_VIS3,
        SPARC_GRP_32BIT,
        SPARC_GRP_64BIT,
    }
}
