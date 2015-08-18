using System;

namespace CSCapstone.SystemZ
{
    public class SystemZDisassembler : Disassembler<SystemZMnemonic, SystemZRegister, SystemZInstructionGroup, SystemZInstructionDetail>
    {
        /// <summary>Create a Capstone X86 Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public SystemZDisassembler(SupportedMode mode)
            : base(SupportedArchitecture.SystemZ, mode)
        {
            return;
        }

        internal override SystemZInstructionDetail CreateDetail(
            Instruction<SystemZMnemonic, SystemZRegister, SystemZInstructionGroup, SystemZInstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            return new SystemZInstructionDetail(from, ref offset);
        }

        protected override Instruction<SystemZMnemonic, SystemZRegister, SystemZInstructionGroup, SystemZInstructionDetail> CreateInstruction(IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<SystemZMnemonic, SystemZRegister, SystemZInstructionGroup, SystemZInstructionDetail>(this, nativeInstruction, ref offset);
        }
    }
}
