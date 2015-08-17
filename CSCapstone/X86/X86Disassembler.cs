using System;

namespace CSCapstone.X86
{
    /// <summary>Capstone X86 Disassembler.</summary>
    public sealed class X86Disassembler : Disassembler<X86Mnemonic, X86Register, X86InstructionGroup, X86InstructionDetail>
    {
        /// <summary>Create a Capstone X86 Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public X86Disassembler(SupportedMode mode)
            : base(SupportedArchitecture.X86, mode)
        {
            return;
        }

        internal override X86InstructionDetail CreateDetail(IntPtr from, ref int offset)
        {
            return new X86InstructionDetail(from, ref offset);
        }

        protected override Instruction<X86Mnemonic, X86Register, X86InstructionGroup, X86InstructionDetail> CreateInstruction(IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<X86Mnemonic, X86Register, X86InstructionGroup, X86InstructionDetail>(this, nativeInstruction, ref offset);
        }
    }
}