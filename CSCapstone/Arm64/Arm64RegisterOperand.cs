using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64RegisterOperand : Arm64Operand
    {
        internal Arm64RegisterOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<Arm64Register>(from, ref offset);
        }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.Register; }
        }

        public Arm64Register Value { get; private set; }
    }
}
