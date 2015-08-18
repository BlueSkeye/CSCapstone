using System;

namespace CSCapstone.Sparc
{
    public class SparcDisassembler : Disassembler<SparcMnemonic, SparcRegister, SparcInstructionGroup, SparcInstructionDetail>
    {
        /// <summary>Create a Capstone X86 Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public SparcDisassembler(SupportedMode mode)
            : base(SupportedArchitecture.Sparc, mode)
        {
            return;
        }

        internal override SparcInstructionDetail CreateDetail(
            Instruction<SparcMnemonic, SparcRegister, SparcInstructionGroup, SparcInstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            return new SparcInstructionDetail(from, ref offset);
        }

        protected override Instruction<SparcMnemonic, SparcRegister, SparcInstructionGroup, SparcInstructionDetail> CreateInstruction(IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<SparcMnemonic, SparcRegister, SparcInstructionGroup, SparcInstructionDetail>(this, nativeInstruction, ref offset);
        }
    }
}
