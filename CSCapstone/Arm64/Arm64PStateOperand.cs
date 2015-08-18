using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64PStateOperand : Arm64Operand
    {
        internal Arm64PStateOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<Arm64PState>(from, ref offset);
        }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.PState; }
        }

        public Arm64PState Value { get; private set; }
    }
}
