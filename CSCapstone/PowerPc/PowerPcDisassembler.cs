using System;

namespace CSCapstone.PowerPc
{
    public class PowerPcDisassembler : Disassembler<PowerPcMnemonic, PowerPcRegister, PowerPcInstructionGroup, PowerPcInstructionDetail>
    {
        /// <summary>Create a Capstone X86 Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public PowerPcDisassembler(SupportedMode mode)
            : base(SupportedArchitecture.PowerPc, mode)
        {
            return;
        }

        internal override PowerPcInstructionDetail CreateDetail(
            Instruction<PowerPcMnemonic, PowerPcRegister, PowerPcInstructionGroup, PowerPcInstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            return new PowerPcInstructionDetail(from, ref offset);
        }

        protected override Instruction<PowerPcMnemonic, PowerPcRegister, PowerPcInstructionGroup, PowerPcInstructionDetail> CreateInstruction(IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<PowerPcMnemonic, PowerPcRegister, PowerPcInstructionGroup, PowerPcInstructionDetail>(this, nativeInstruction, ref offset);
        }
    }
}
