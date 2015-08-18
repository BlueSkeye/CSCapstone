using System;

namespace CSCapstone.Mips
{
    public enum MipsInstructionGroup
    {
        MIPS_GRP_INVALID = 0, // = CS_GRP_INVALID

        //> Generic groups
        // all jump instructions (conditional+direct+indirect jumps)
        MIPS_GRP_JUMP,	// = CS_GRP_JUMP

        //> Architecture-specific groups
        MIPS_GRP_BITCOUNT = 128,
        MIPS_GRP_DSP,
        MIPS_GRP_DSPR2,
        MIPS_GRP_FPIDX,
        MIPS_GRP_MSA,
        MIPS_GRP_MIPS32R2,
        MIPS_GRP_MIPS64,
        MIPS_GRP_MIPS64R2,
        MIPS_GRP_SEINREG,
        MIPS_GRP_STDENC,
        MIPS_GRP_SWAP,
        MIPS_GRP_MICROMIPS,
        MIPS_GRP_MIPS16MODE,
        MIPS_GRP_FP64BIT,
        MIPS_GRP_NONANSFPMATH,
        MIPS_GRP_NOTFP64BIT,
        MIPS_GRP_NOTINMICROMIPS,
        MIPS_GRP_NOTNACL,
        MIPS_GRP_NOTMIPS32R6,
        MIPS_GRP_NOTMIPS64R6,
        MIPS_GRP_CNMIPS,
        MIPS_GRP_MIPS32,
        MIPS_GRP_MIPS32R6,
        MIPS_GRP_MIPS64R6,
        MIPS_GRP_MIPS2,
        MIPS_GRP_MIPS3,
        MIPS_GRP_MIPS3_32,
        MIPS_GRP_MIPS3_32R2,
        MIPS_GRP_MIPS4_32,
        MIPS_GRP_MIPS4_32R2,
        MIPS_GRP_MIPS5_32R2,
        MIPS_GRP_GP32BIT,
        MIPS_GRP_GP64BIT,

        MIPS_GRP_ENDING,
    }
}
