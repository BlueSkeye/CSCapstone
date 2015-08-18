using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64MrsRegisterOperand : Arm64Operand
    {
        internal Arm64MrsRegisterOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<Arm64MrsRegister>(from, ref offset);
            return;
        }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.MrsRegister; }
        }

        public Arm64MrsRegister Value { get; private set; }
    }
}
