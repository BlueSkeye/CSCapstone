using System;

namespace CSCapstone.Sparc
{
    public sealed class SparcRegisterOperand : SparcOperand
    {
        internal SparcRegisterOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetEnum<SparcRegister>(from, ref offset);
        }

        public override SparcOperandType Type
        {
            get { return SparcOperandType.SPARC_OP_REG; }
        }

        public SparcRegister Value { get; private set; }
    }
}
