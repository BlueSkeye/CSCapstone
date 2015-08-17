using System;

namespace CSCapstone.Arm
{
    public sealed class ArmSetEndOperand : ArmOperand
    {
        internal ArmSetEndOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<ArmSetEndOperandType>(from, ref offset);
            return;
        }

        public override ArmOperandType Type
        {
            get { return ArmOperandType.SetEnd; }
        }

        public ArmSetEndOperandType Value { get; private set; }
    }
}
