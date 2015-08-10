﻿// ReSharper disable InconsistentNaming

namespace CSCapstone.X86 {
    /// <summary>
    ///     X86 Instruction.
    /// </summary>
    public enum X86Instruction {
        /// <summary>
        ///     Invalid X86 Instruction.
        /// </summary>
        Invalid = 0,

        AAA,
        AAD,
        AAM,
        AAS,
        FABS,
        ADC,
        ADCX,
        ADD,
        ADDPD,
        ADDPS,
        ADDSD,
        ADDSS,
        ADDSUBPD,
        ADDSUBPS,
        FADD,
        FIADD,
        FADDP,
        ADOX,
        AESDECLAST,
        AESDEC,
        AESENCLAST,
        AESENC,
        AESIMC,
        AESKEYGENASSIST,
        AND,
        ANDN,
        ANDNPD,
        ANDNPS,
        ANDPD,
        ANDPS,
        ARPL,
        BEXTR,
        BLCFILL,
        BLCI,
        BLCIC,
        BLCMSK,
        BLCS,
        BLENDPD,
        BLENDPS,
        BLENDVPD,
        BLENDVPS,
        BLSFILL,
        BLSI,
        BLSIC,
        BLSMSK,
        BLSR,
        BOUND,
        BSF,
        BSR,
        BSWAP,
        BT,
        BTC,
        BTR,
        BTS,
        BZHI,
        CALL,
        CBW,
        CDQ,
        CDQE,
        FCHS,
        CLAC,
        CLC,
        CLD,
        CLFLUSH,
        CLGI,
        CLI,
        CLTS,
        CMC,
        CMOVA,
        CMOVAE,
        CMOVB,
        CMOVBE,
        FCMOVBE,
        FCMOVB,
        CMOVE,
        FCMOVE,
        CMOVG,
        CMOVGE,
        CMOVL,
        CMOVLE,
        FCMOVNBE,
        FCMOVNB,
        CMOVNE,
        FCMOVNE,
        CMOVNO,
        CMOVNP,
        FCMOVNU,
        CMOVNS,
        CMOVO,
        CMOVP,
        FCMOVU,
        CMOVS,
        CMP,
        CMPPD,
        CMPPS,
        CMPSB,
        CMPSD,
        CMPSQ,
        CMPSS,
        CMPSW,
        CMPXCHG16B,
        CMPXCHG,
        CMPXCHG8B,
        COMISD,
        COMISS,
        FCOMP,
        FCOMPI,
        FCOMI,
        FCOM,
        FCOS,
        CPUID,
        CQO,
        CRC32,
        CVTDQ2PD,
        CVTDQ2PS,
        CVTPD2DQ,
        CVTPD2PS,
        CVTPS2DQ,
        CVTPS2PD,
        CVTSD2SI,
        CVTSD2SS,
        CVTSI2SD,
        CVTSI2SS,
        CVTSS2SD,
        CVTSS2SI,
        CVTTPD2DQ,
        CVTTPS2DQ,
        CVTTSD2SI,
        CVTTSS2SI,
        CWD,
        CWDE,
        DAA,
        DAS,
        DATA16,
        DEC,
        DIV,
        DIVPD,
        DIVPS,
        FDIVR,
        FIDIVR,
        FDIVRP,
        DIVSD,
        DIVSS,
        FDIV,
        FIDIV,
        FDIVP,
        DPPD,
        DPPS,
        RET,
        ENCLS,
        ENCLU,
        ENTER,
        EXTRACTPS,
        EXTRQ,
        F2XM1,
        LCALL,
        LJMP,
        FBLD,
        FBSTP,
        FCOMPP,
        FDECSTP,
        FEMMS,
        FFREE,
        FICOM,
        FICOMP,
        FINCSTP,
        FLDCW,
        FLDENV,
        FLDL2E,
        FLDL2T,
        FLDLG2,
        FLDLN2,
        FLDPI,
        FNCLEX,
        FNINIT,
        FNOP,
        FNSTCW,
        FNSTSW,
        FPATAN,
        FPREM,
        FPREM1,
        FPTAN,
        FRNDINT,
        FRSTOR,
        FNSAVE,
        FSCALE,
        FSETPM,
        FSINCOS,
        FNSTENV,
        FXAM,
        FXRSTOR,
        FXRSTOR64,
        FXSAVE,
        FXSAVE64,
        FXTRACT,
        FYL2X,
        FYL2XP1,
        MOVAPD,
        MOVAPS,
        ORPD,
        ORPS,
        VMOVAPD,
        VMOVAPS,
        XORPD,
        XORPS,
        GETSEC,
        HADDPD,
        HADDPS,
        HLT,
        HSUBPD,
        HSUBPS,
        IDIV,
        FILD,
        IMUL,
        IN,
        INC,
        INSB,
        INSERTPS,
        INSERTQ,
        INSD,
        INSW,
        INT,
        INT1,
        INT3,
        INTO,
        INVD,
        INVEPT,
        INVLPG,
        INVLPGA,
        INVPCID,
        INVVPID,
        IRET,
        IRETD,
        IRETQ,
        FISTTP,
        FIST,
        FISTP,
        UCOMISD,
        UCOMISS,
        VCMP,
        VCOMISD,
        VCOMISS,
        VCVTSD2SS,
        VCVTSI2SD,
        VCVTSI2SS,
        VCVTSS2SD,
        VCVTTSD2SI,
        VCVTTSD2USI,
        VCVTTSS2SI,
        VCVTTSS2USI,
        VCVTUSI2SD,
        VCVTUSI2SS,
        VUCOMISD,
        VUCOMISS,
        JAE,
        JA,
        JBE,
        JB,
        JCXZ,
        JECXZ,
        JE,
        JGE,
        JG,
        JLE,
        JL,
        JMP,
        JNE,
        JNO,
        JNP,
        JNS,
        JO,
        JP,
        JRCXZ,
        JS,
        KANDB,
        KANDD,
        KANDNB,
        KANDND,
        KANDNQ,
        KANDNW,
        KANDQ,
        KANDW,
        KMOVB,
        KMOVD,
        KMOVQ,
        KMOVW,
        KNOTB,
        KNOTD,
        KNOTQ,
        KNOTW,
        KORB,
        KORD,
        KORQ,
        KORTESTW,
        KORW,
        KSHIFTLW,
        KSHIFTRW,
        KUNPCKBW,
        KXNORB,
        KXNORD,
        KXNORQ,
        KXNORW,
        KXORB,
        KXORD,
        KXORQ,
        KXORW,
        LAHF,
        LAR,
        LDDQU,
        LDMXCSR,
        LDS,
        FLDZ,
        FLD1,
        FLD,
        LEA,
        LEAVE,
        LES,
        LFENCE,
        LFS,
        LGDT,
        LGS,
        LIDT,
        LLDT,
        LMSW,
        OR,
        SUB,
        XOR,
        LODSB,
        LODSD,
        LODSQ,
        LODSW,
        LOOP,
        LOOPE,
        LOOPNE,
        RETF,
        RETFQ,
        LSL,
        LSS,
        LTR,
        XADD,
        LZCNT,
        MASKMOVDQU,
        MAXPD,
        MAXPS,
        MAXSD,
        MAXSS,
        MFENCE,
        MINPD,
        MINPS,
        MINSD,
        MINSS,
        CVTPD2PI,
        CVTPI2PD,
        CVTPI2PS,
        CVTPS2PI,
        CVTTPD2PI,
        CVTTPS2PI,
        EMMS,
        MASKMOVQ,
        MOVD,
        MOVDQ2Q,
        MOVNTQ,
        MOVQ2DQ,
        MOVQ,
        PABSB,
        PABSD,
        PABSW,
        PACKSSDW,
        PACKSSWB,
        PACKUSWB,
        PADDB,
        PADDD,
        PADDQ,
        PADDSB,
        PADDSW,
        PADDUSB,
        PADDUSW,
        PADDW,
        PALIGNR,
        PANDN,
        PAND,
        PAVGB,
        PAVGW,
        PCMPEQB,
        PCMPEQD,
        PCMPEQW,
        PCMPGTB,
        PCMPGTD,
        PCMPGTW,
        PEXTRW,
        PHADDSW,
        PHADDW,
        PHADDD,
        PHSUBD,
        PHSUBSW,
        PHSUBW,
        PINSRW,
        PMADDUBSW,
        PMADDWD,
        PMAXSW,
        PMAXUB,
        PMINSW,
        PMINUB,
        PMOVMSKB,
        PMULHRSW,
        PMULHUW,
        PMULHW,
        PMULLW,
        PMULUDQ,
        POR,
        PSADBW,
        PSHUFB,
        PSHUFW,
        PSIGNB,
        PSIGND,
        PSIGNW,
        PSLLD,
        PSLLQ,
        PSLLW,
        PSRAD,
        PSRAW,
        PSRLD,
        PSRLQ,
        PSRLW,
        PSUBB,
        PSUBD,
        PSUBQ,
        PSUBSB,
        PSUBSW,
        PSUBUSB,
        PSUBUSW,
        PSUBW,
        PUNPCKHBW,
        PUNPCKHDQ,
        PUNPCKHWD,
        PUNPCKLBW,
        PUNPCKLDQ,
        PUNPCKLWD,
        PXOR,
        MONITOR,
        MONTMUL,
        MOV,
        MOVABS,
        MOVBE,
        MOVDDUP,
        MOVDQA,
        MOVDQU,
        MOVHLPS,
        MOVHPD,
        MOVHPS,
        MOVLHPS,
        MOVLPD,
        MOVLPS,
        MOVMSKPD,
        MOVMSKPS,
        MOVNTDQA,
        MOVNTDQ,
        MOVNTI,
        MOVNTPD,
        MOVNTPS,
        MOVNTSD,
        MOVNTSS,
        MOVSB,
        MOVSD,
        MOVSHDUP,
        MOVSLDUP,
        MOVSQ,
        MOVSS,
        MOVSW,
        MOVSX,
        MOVSXD,
        MOVUPD,
        MOVUPS,
        MOVZX,
        MPSADBW,
        MUL,
        MULPD,
        MULPS,
        MULSD,
        MULSS,
        MULX,
        FMUL,
        FIMUL,
        FMULP,
        MWAIT,
        NEG,
        NOP,
        NOT,
        OUT,
        OUTSB,
        OUTSD,
        OUTSW,
        PACKUSDW,
        PAUSE,
        PAVGUSB,
        PBLENDVB,
        PBLENDW,
        PCLMULQDQ,
        PCMPEQQ,
        PCMPESTRI,
        PCMPESTRM,
        PCMPGTQ,
        PCMPISTRI,
        PCMPISTRM,
        PDEP,
        PEXT,
        PEXTRB,
        PEXTRD,
        PEXTRQ,
        PF2ID,
        PF2IW,
        PFACC,
        PFADD,
        PFCMPEQ,
        PFCMPGE,
        PFCMPGT,
        PFMAX,
        PFMIN,
        PFMUL,
        PFNACC,
        PFPNACC,
        PFRCPIT1,
        PFRCPIT2,
        PFRCP,
        PFRSQIT1,
        PFRSQRT,
        PFSUBR,
        PFSUB,
        PHMINPOSUW,
        PI2FD,
        PI2FW,
        PINSRB,
        PINSRD,
        PINSRQ,
        PMAXSB,
        PMAXSD,
        PMAXUD,
        PMAXUW,
        PMINSB,
        PMINSD,
        PMINUD,
        PMINUW,
        PMOVSXBD,
        PMOVSXBQ,
        PMOVSXBW,
        PMOVSXDQ,
        PMOVSXWD,
        PMOVSXWQ,
        PMOVZXBD,
        PMOVZXBQ,
        PMOVZXBW,
        PMOVZXDQ,
        PMOVZXWD,
        PMOVZXWQ,
        PMULDQ,
        PMULHRW,
        PMULLD,
        POP,
        POPAW,
        POPAL,
        POPCNT,
        POPF,
        POPFD,
        POPFQ,
        PREFETCH,
        PREFETCHNTA,
        PREFETCHT0,
        PREFETCHT1,
        PREFETCHT2,
        PREFETCHW,
        PSHUFD,
        PSHUFHW,
        PSHUFLW,
        PSLLDQ,
        PSRLDQ,
        PSWAPD,
        PTEST,
        PUNPCKHQDQ,
        PUNPCKLQDQ,
        PUSH,
        PUSHAW,
        PUSHAL,
        PUSHF,
        PUSHFD,
        PUSHFQ,
        RCL,
        RCPPS,
        RCPSS,
        RCR,
        RDFSBASE,
        RDGSBASE,
        RDMSR,
        RDPMC,
        RDRAND,
        RDSEED,
        RDTSC,
        RDTSCP,
        ROL,
        ROR,
        RORX,
        ROUNDPD,
        ROUNDPS,
        ROUNDSD,
        ROUNDSS,
        RSM,
        RSQRTPS,
        RSQRTSS,
        SAHF,
        SAL,
        SALC,
        SAR,
        SARX,
        SBB,
        SCASB,
        SCASD,
        SCASQ,
        SCASW,
        SETAE,
        SETA,
        SETBE,
        SETB,
        SETE,
        SETGE,
        SETG,
        SETLE,
        SETL,
        SETNE,
        SETNO,
        SETNP,
        SETNS,
        SETO,
        SETP,
        SETS,
        SFENCE,
        SGDT,
        SHA1MSG1,
        SHA1MSG2,
        SHA1NEXTE,
        SHA1RNDS4,
        SHA256MSG1,
        SHA256MSG2,
        SHA256RNDS2,
        SHL,
        SHLD,
        SHLX,
        SHR,
        SHRD,
        SHRX,
        SHUFPD,
        SHUFPS,
        SIDT,
        FSIN,
        SKINIT,
        SLDT,
        SMSW,
        SQRTPD,
        SQRTPS,
        SQRTSD,
        SQRTSS,
        FSQRT,
        STAC,
        STC,
        STD,
        STGI,
        STI,
        STMXCSR,
        STOSB,
        STOSD,
        STOSQ,
        STOSW,
        STR,
        FST,
        FSTP,
        FSTPNCE,
        SUBPD,
        SUBPS,
        FSUBR,
        FISUBR,
        FSUBRP,
        SUBSD,
        SUBSS,
        FSUB,
        FISUB,
        FSUBP,
        SWAPGS,
        SYSCALL,
        SYSENTER,
        SYSEXIT,
        SYSRET,
        T1MSKC,
        TEST,
        UD2,
        FTST,
        TZCNT,
        TZMSK,
        FUCOMPI,
        FUCOMI,
        FUCOMPP,
        FUCOMP,
        FUCOM,
        UD2B,
        UNPCKHPD,
        UNPCKHPS,
        UNPCKLPD,
        UNPCKLPS,
        VADDPD,
        VADDPS,
        VADDSD,
        VADDSS,
        VADDSUBPD,
        VADDSUBPS,
        VAESDECLAST,
        VAESDEC,
        VAESENCLAST,
        VAESENC,
        VAESIMC,
        VAESKEYGENASSIST,
        VALIGND,
        VALIGNQ,
        VANDNPD,
        VANDNPS,
        VANDPD,
        VANDPS,
        VBLENDMPD,
        VBLENDMPS,
        VBLENDPD,
        VBLENDPS,
        VBLENDVPD,
        VBLENDVPS,
        VBROADCASTF128,
        VBROADCASTI128,
        VBROADCASTI32X4,
        VBROADCASTI64X4,
        VBROADCASTSD,
        VBROADCASTSS,
        VCMPPD,
        VCMPPS,
        VCMPSD,
        VCMPSS,
        VCVTDQ2PD,
        VCVTDQ2PS,
        VCVTPD2DQX,
        VCVTPD2DQ,
        VCVTPD2PSX,
        VCVTPD2PS,
        VCVTPD2UDQ,
        VCVTPH2PS,
        VCVTPS2DQ,
        VCVTPS2PD,
        VCVTPS2PH,
        VCVTPS2UDQ,
        VCVTSD2SI,
        VCVTSD2USI,
        VCVTSS2SI,
        VCVTSS2USI,
        VCVTTPD2DQX,
        VCVTTPD2DQ,
        VCVTTPD2UDQ,
        VCVTTPS2DQ,
        VCVTTPS2UDQ,
        VCVTUDQ2PD,
        VCVTUDQ2PS,
        VDIVPD,
        VDIVPS,
        VDIVSD,
        VDIVSS,
        VDPPD,
        VDPPS,
        VERR,
        VERW,
        VEXTRACTF128,
        VEXTRACTF32X4,
        VEXTRACTF64X4,
        VEXTRACTI128,
        VEXTRACTI32X4,
        VEXTRACTI64X4,
        VEXTRACTPS,
        VFMADD132PD,
        VFMADD132PS,
        VFMADD213PD,
        VFMADD213PS,
        VFMADDPD,
        VFMADD231PD,
        VFMADDPS,
        VFMADD231PS,
        VFMADDSD,
        VFMADD213SD,
        VFMADD132SD,
        VFMADD231SD,
        VFMADDSS,
        VFMADD213SS,
        VFMADD132SS,
        VFMADD231SS,
        VFMADDSUB132PD,
        VFMADDSUB132PS,
        VFMADDSUB213PD,
        VFMADDSUB213PS,
        VFMADDSUBPD,
        VFMADDSUB231PD,
        VFMADDSUBPS,
        VFMADDSUB231PS,
        VFMSUB132PD,
        VFMSUB132PS,
        VFMSUB213PD,
        VFMSUB213PS,
        VFMSUBADD132PD,
        VFMSUBADD132PS,
        VFMSUBADD213PD,
        VFMSUBADD213PS,
        VFMSUBADDPD,
        VFMSUBADD231PD,
        VFMSUBADDPS,
        VFMSUBADD231PS,
        VFMSUBPD,
        VFMSUB231PD,
        VFMSUBPS,
        VFMSUB231PS,
        VFMSUBSD,
        VFMSUB213SD,
        VFMSUB132SD,
        VFMSUB231SD,
        VFMSUBSS,
        VFMSUB213SS,
        VFMSUB132SS,
        VFMSUB231SS,
        VFNMADD132PD,
        VFNMADD132PS,
        VFNMADD213PD,
        VFNMADD213PS,
        VFNMADDPD,
        VFNMADD231PD,
        VFNMADDPS,
        VFNMADD231PS,
        VFNMADDSD,
        VFNMADD213SD,
        VFNMADD132SD,
        VFNMADD231SD,
        VFNMADDSS,
        VFNMADD213SS,
        VFNMADD132SS,
        VFNMADD231SS,
        VFNMSUB132PD,
        VFNMSUB132PS,
        VFNMSUB213PD,
        VFNMSUB213PS,
        VFNMSUBPD,
        VFNMSUB231PD,
        VFNMSUBPS,
        VFNMSUB231PS,
        VFNMSUBSD,
        VFNMSUB213SD,
        VFNMSUB132SD,
        VFNMSUB231SD,
        VFNMSUBSS,
        VFNMSUB213SS,
        VFNMSUB132SS,
        VFNMSUB231SS,
        VFRCZPD,
        VFRCZPS,
        VFRCZSD,
        VFRCZSS,
        VORPD,
        VORPS,
        VXORPD,
        VXORPS,
        VGATHERDPD,
        VGATHERDPS,
        VGATHERPF0DPD,
        VGATHERPF0DPS,
        VGATHERPF0QPD,
        VGATHERPF0QPS,
        VGATHERPF1DPD,
        VGATHERPF1DPS,
        VGATHERPF1QPD,
        VGATHERPF1QPS,
        VGATHERQPD,
        VGATHERQPS,
        VHADDPD,
        VHADDPS,
        VHSUBPD,
        VHSUBPS,
        VINSERTF128,
        VINSERTF32X4,
        VINSERTF64X4,
        VINSERTI128,
        VINSERTI32X4,
        VINSERTI64X4,
        VINSERTPS,
        VLDDQU,
        VLDMXCSR,
        VMASKMOVDQU,
        VMASKMOVPD,
        VMASKMOVPS,
        VMAXPD,
        VMAXPS,
        VMAXSD,
        VMAXSS,
        VMCALL,
        VMCLEAR,
        VMFUNC,
        VMINPD,
        VMINPS,
        VMINSD,
        VMINSS,
        VMLAUNCH,
        VMLOAD,
        VMMCALL,
        VMOVQ,
        VMOVDDUP,
        VMOVD,
        VMOVDQA32,
        VMOVDQA64,
        VMOVDQA,
        VMOVDQU16,
        VMOVDQU32,
        VMOVDQU64,
        VMOVDQU8,
        VMOVDQU,
        VMOVHLPS,
        VMOVHPD,
        VMOVHPS,
        VMOVLHPS,
        VMOVLPD,
        VMOVLPS,
        VMOVMSKPD,
        VMOVMSKPS,
        VMOVNTDQA,
        VMOVNTDQ,
        VMOVNTPD,
        VMOVNTPS,
        VMOVSD,
        VMOVSHDUP,
        VMOVSLDUP,
        VMOVSS,
        VMOVUPD,
        VMOVUPS,
        VMPSADBW,
        VMPTRLD,
        VMPTRST,
        VMREAD,
        VMRESUME,
        VMRUN,
        VMSAVE,
        VMULPD,
        VMULPS,
        VMULSD,
        VMULSS,
        VMWRITE,
        VMXOFF,
        VMXON,
        VPABSB,
        VPABSD,
        VPABSQ,
        VPABSW,
        VPACKSSDW,
        VPACKSSWB,
        VPACKUSDW,
        VPACKUSWB,
        VPADDB,
        VPADDD,
        VPADDQ,
        VPADDSB,
        VPADDSW,
        VPADDUSB,
        VPADDUSW,
        VPADDW,
        VPALIGNR,
        VPANDD,
        VPANDND,
        VPANDNQ,
        VPANDN,
        VPANDQ,
        VPAND,
        VPAVGB,
        VPAVGW,
        VPBLENDD,
        VPBLENDMD,
        VPBLENDMQ,
        VPBLENDVB,
        VPBLENDW,
        VPBROADCASTB,
        VPBROADCASTD,
        VPBROADCASTMB2Q,
        VPBROADCASTMW2D,
        VPBROADCASTQ,
        VPBROADCASTW,
        VPCLMULQDQ,
        VPCMOV,
        VPCMP,
        VPCMPD,
        VPCMPEQB,
        VPCMPEQD,
        VPCMPEQQ,
        VPCMPEQW,
        VPCMPESTRI,
        VPCMPESTRM,
        VPCMPGTB,
        VPCMPGTD,
        VPCMPGTQ,
        VPCMPGTW,
        VPCMPISTRI,
        VPCMPISTRM,
        VPCMPQ,
        VPCMPUD,
        VPCMPUQ,
        VPCOMB,
        VPCOMD,
        VPCOMQ,
        VPCOMUB,
        VPCOMUD,
        VPCOMUQ,
        VPCOMUW,
        VPCOMW,
        VPCONFLICTD,
        VPCONFLICTQ,
        VPERM2F128,
        VPERM2I128,
        VPERMD,
        VPERMI2D,
        VPERMI2PD,
        VPERMI2PS,
        VPERMI2Q,
        VPERMIL2PD,
        VPERMIL2PS,
        VPERMILPD,
        VPERMILPS,
        VPERMPD,
        VPERMPS,
        VPERMQ,
        VPERMT2D,
        VPERMT2PD,
        VPERMT2PS,
        VPERMT2Q,
        VPEXTRB,
        VPEXTRD,
        VPEXTRQ,
        VPEXTRW,
        VPGATHERDD,
        VPGATHERDQ,
        VPGATHERQD,
        VPGATHERQQ,
        VPHADDBD,
        VPHADDBQ,
        VPHADDBW,
        VPHADDDQ,
        VPHADDD,
        VPHADDSW,
        VPHADDUBD,
        VPHADDUBQ,
        VPHADDUBW,
        VPHADDUDQ,
        VPHADDUWD,
        VPHADDUWQ,
        VPHADDWD,
        VPHADDWQ,
        VPHADDW,
        VPHMINPOSUW,
        VPHSUBBW,
        VPHSUBDQ,
        VPHSUBD,
        VPHSUBSW,
        VPHSUBWD,
        VPHSUBW,
        VPINSRB,
        VPINSRD,
        VPINSRQ,
        VPINSRW,
        VPLZCNTD,
        VPLZCNTQ,
        VPMACSDD,
        VPMACSDQH,
        VPMACSDQL,
        VPMACSSDD,
        VPMACSSDQH,
        VPMACSSDQL,
        VPMACSSWD,
        VPMACSSWW,
        VPMACSWD,
        VPMACSWW,
        VPMADCSSWD,
        VPMADCSWD,
        VPMADDUBSW,
        VPMADDWD,
        VPMASKMOVD,
        VPMASKMOVQ,
        VPMAXSB,
        VPMAXSD,
        VPMAXSQ,
        VPMAXSW,
        VPMAXUB,
        VPMAXUD,
        VPMAXUQ,
        VPMAXUW,
        VPMINSB,
        VPMINSD,
        VPMINSQ,
        VPMINSW,
        VPMINUB,
        VPMINUD,
        VPMINUQ,
        VPMINUW,
        VPMOVDB,
        VPMOVDW,
        VPMOVMSKB,
        VPMOVQB,
        VPMOVQD,
        VPMOVQW,
        VPMOVSDB,
        VPMOVSDW,
        VPMOVSQB,
        VPMOVSQD,
        VPMOVSQW,
        VPMOVSXBD,
        VPMOVSXBQ,
        VPMOVSXBW,
        VPMOVSXDQ,
        VPMOVSXWD,
        VPMOVSXWQ,
        VPMOVUSDB,
        VPMOVUSDW,
        VPMOVUSQB,
        VPMOVUSQD,
        VPMOVUSQW,
        VPMOVZXBD,
        VPMOVZXBQ,
        VPMOVZXBW,
        VPMOVZXDQ,
        VPMOVZXWD,
        VPMOVZXWQ,
        VPMULDQ,
        VPMULHRSW,
        VPMULHUW,
        VPMULHW,
        VPMULLD,
        VPMULLW,
        VPMULUDQ,
        VPORD,
        VPORQ,
        VPOR,
        VPPERM,
        VPROTB,
        VPROTD,
        VPROTQ,
        VPROTW,
        VPSADBW,
        VPSCATTERDD,
        VPSCATTERDQ,
        VPSCATTERQD,
        VPSCATTERQQ,
        VPSHAB,
        VPSHAD,
        VPSHAQ,
        VPSHAW,
        VPSHLB,
        VPSHLD,
        VPSHLQ,
        VPSHLW,
        VPSHUFB,
        VPSHUFD,
        VPSHUFHW,
        VPSHUFLW,
        VPSIGNB,
        VPSIGND,
        VPSIGNW,
        VPSLLDQ,
        VPSLLD,
        VPSLLQ,
        VPSLLVD,
        VPSLLVQ,
        VPSLLW,
        VPSRAD,
        VPSRAQ,
        VPSRAVD,
        VPSRAVQ,
        VPSRAW,
        VPSRLDQ,
        VPSRLD,
        VPSRLQ,
        VPSRLVD,
        VPSRLVQ,
        VPSRLW,
        VPSUBB,
        VPSUBD,
        VPSUBQ,
        VPSUBSB,
        VPSUBSW,
        VPSUBUSB,
        VPSUBUSW,
        VPSUBW,
        VPTESTMD,
        VPTESTMQ,
        VPTESTNMD,
        VPTESTNMQ,
        VPTEST,
        VPUNPCKHBW,
        VPUNPCKHDQ,
        VPUNPCKHQDQ,
        VPUNPCKHWD,
        VPUNPCKLBW,
        VPUNPCKLDQ,
        VPUNPCKLQDQ,
        VPUNPCKLWD,
        VPXORD,
        VPXORQ,
        VPXOR,
        VRCP14PD,
        VRCP14PS,
        VRCP14SD,
        VRCP14SS,
        VRCP28PD,
        VRCP28PS,
        VRCP28SD,
        VRCP28SS,
        VRCPPS,
        VRCPSS,
        VRNDSCALEPD,
        VRNDSCALEPS,
        VRNDSCALESD,
        VRNDSCALESS,
        VROUNDPD,
        VROUNDPS,
        VROUNDSD,
        VROUNDSS,
        VRSQRT14PD,
        VRSQRT14PS,
        VRSQRT14SD,
        VRSQRT14SS,
        VRSQRT28PD,
        VRSQRT28PS,
        VRSQRT28SD,
        VRSQRT28SS,
        VRSQRTPS,
        VRSQRTSS,
        VSCATTERDPD,
        VSCATTERDPS,
        VSCATTERPF0DPD,
        VSCATTERPF0DPS,
        VSCATTERPF0QPD,
        VSCATTERPF0QPS,
        VSCATTERPF1DPD,
        VSCATTERPF1DPS,
        VSCATTERPF1QPD,
        VSCATTERPF1QPS,
        VSCATTERQPD,
        VSCATTERQPS,
        VSHUFPD,
        VSHUFPS,
        VSQRTPD,
        VSQRTPS,
        VSQRTSD,
        VSQRTSS,
        VSTMXCSR,
        VSUBPD,
        VSUBPS,
        VSUBSD,
        VSUBSS,
        VTESTPD,
        VTESTPS,
        VUNPCKHPD,
        VUNPCKHPS,
        VUNPCKLPD,
        VUNPCKLPS,
        VZEROALL,
        VZEROUPPER,
        WAIT,
        WBINVD,
        WRFSBASE,
        WRGSBASE,
        WRMSR,
        XABORT,
        XACQUIRE,
        XBEGIN,
        XCHG,
        FXCH,
        XCRYPTCBC,
        XCRYPTCFB,
        XCRYPTCTR,
        XCRYPTECB,
        XCRYPTOFB,
        XEND,
        XGETBV,
        XLATB,
        XRELEASE,
        XRSTOR,
        XRSTOR64,
        XSAVE,
        XSAVE64,
        XSAVEOPT,
        XSAVEOPT64,
        XSETBV,
        XSHA1,
        XSHA256,
        XSTORE,
        XTEST
    }
}