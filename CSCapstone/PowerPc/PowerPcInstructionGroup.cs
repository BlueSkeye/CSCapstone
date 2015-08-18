using System;

namespace CSCapstone.PowerPc
{
    public enum PowerPcInstructionGroup
    {
        PPC_GRP_INVALID = 0, // = CS_GRP_INVALID

        //> Generic groups
        // all jump instructions (conditional+direct+indirect jumps)
        PPC_GRP_JUMP,	// = CS_GRP_JUMP

        //> Architecture-specific groups
        PPC_GRP_ALTIVEC = 128,
        PPC_GRP_MODE32,
        PPC_GRP_MODE64,
        PPC_GRP_BOOKE,
        PPC_GRP_NOTBOOKE,
        PPC_GRP_SPE,
        PPC_GRP_VSX,
        PPC_GRP_E500,
        PPC_GRP_PPC4XX,
        PPC_GRP_PPC6XX,
    }
}
