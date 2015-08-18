using System;

namespace CSCapstone.XCore
{
    public sealed class XCoreDisassembler : Disassembler<XCoreMnemonic, XCoreRegister, XCoreInstructionGroup, XCoreInstructionDetail>
    {
        /// <summary>Create a Capstone X86 Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public XCoreDisassembler(SupportedMode mode)
            : base(SupportedArchitecture.XCore, mode)
        {
            return;
        }

        internal override XCoreInstructionDetail CreateDetail(
            Instruction<XCoreMnemonic, XCoreRegister, XCoreInstructionGroup, XCoreInstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            return new XCoreInstructionDetail(from, ref offset);
        }

        protected override Instruction<XCoreMnemonic, XCoreRegister, XCoreInstructionGroup, XCoreInstructionDetail> CreateInstruction(IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<XCoreMnemonic, XCoreRegister, XCoreInstructionGroup, XCoreInstructionDetail>(this, nativeInstruction, ref offset);
        }
    }
}
