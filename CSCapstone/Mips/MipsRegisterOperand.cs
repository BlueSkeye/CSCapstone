using System;

namespace CSCapstone.Mips
{
    public sealed class MipsRegisterOperand : MipsOperand
    {
        internal MipsRegisterOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetEnum<MipsRegister>(from, ref offset);
        }

        public override MipsOperandType Type
        {
            get { return MipsOperandType.MIPS_OP_REG; }
        }

        public MipsRegister Value { get; private set; }
    }
}
