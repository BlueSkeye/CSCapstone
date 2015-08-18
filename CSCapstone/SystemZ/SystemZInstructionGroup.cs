using System;

namespace CSCapstone.SystemZ
{
    public enum SystemZInstructionGroup
    {
        SYSZ_GRP_INVALID = 0, // = CS_GRP_INVALID

        //> Generic groups
        // all jump instructions (conditional+direct+indirect jumps)
        SYSZ_GRP_JUMP,	// = CS_GRP_JUMP

        //> Architecture-specific groups
        SYSZ_GRP_DISTINCTOPS = 128,
        SYSZ_GRP_FPEXTENSION,
        SYSZ_GRP_HIGHWORD,
        SYSZ_GRP_INTERLOCKEDACCESS1,
        SYSZ_GRP_LOADSTOREONCOND,
    }
}
