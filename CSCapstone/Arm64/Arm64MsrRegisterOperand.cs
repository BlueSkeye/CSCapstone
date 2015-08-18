using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64MsrRegisterOperand : Arm64Operand
    {
        internal Arm64MsrRegisterOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<Arm64MsrRegister>(from, ref offset);
            return;
        }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.MsrRegister; }
        }

        public Arm64MsrRegister Value { get; private set; }
    }
}
