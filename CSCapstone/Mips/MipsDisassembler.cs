using System;

namespace CSCapstone.Mips
{
    /// <summary>Capstone X86 Disassembler.</summary>
    public sealed class MipsDisassembler : Disassembler<MipsMnemonic, MipsRegister, MipsInstructionGroup, MipsInstructionDetail>
    {
        /// <summary>Create a Capstone X86 Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public MipsDisassembler(SupportedMode mode)
            : base(SupportedArchitecture.Mips, mode)
        {
            return;
        }

        internal override MipsInstructionDetail CreateDetail(
            Instruction<MipsMnemonic, MipsRegister, MipsInstructionGroup, MipsInstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            return new MipsInstructionDetail(from, ref offset);
        }

        protected override Instruction<MipsMnemonic, MipsRegister, MipsInstructionGroup, MipsInstructionDetail> CreateInstruction(IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<MipsMnemonic, MipsRegister, MipsInstructionGroup, MipsInstructionDetail>(this, nativeInstruction, ref offset);
        }
    }
}
