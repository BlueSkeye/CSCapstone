using System;

namespace CSCapstone.Arm
{
    /// <summary>Capstone ARM Disassembler.</summary>
    public sealed class ArmDisassembler : Disassembler<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail>
    {
        /// <summary>Create a Capstone ARM Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public ArmDisassembler(SupportedMode mode)
            : base(SupportedArchitecture.Arm, mode)
        {
            return;
        }

        internal override ArmInstructionDetail CreateDetail(IntPtr from, ref int offset)
        {
            return new ArmInstructionDetail(from, ref offset);
        }

        protected override Instruction<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail> CreateInstruction(System.IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail>(this, nativeInstruction, ref offset);
        }
    }
}