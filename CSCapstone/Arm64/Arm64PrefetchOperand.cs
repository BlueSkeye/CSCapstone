using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64PrefetchOperand : Arm64Operand
    {
        internal Arm64PrefetchOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<Arm64PrefetchOperation>(from, ref offset);
            return;
        }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.PrefetchOperation; }
        }

        public Arm64PrefetchOperation Value { get; private set; }
    }
}
