using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64MemoryBarrierOperand : Arm64Operand
    {
        internal Arm64MemoryBarrierOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<Arm64MemoryBarrierOperation>(from, ref offset);
            return;
        }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.MemoryBarrierOperation; }
        }

        public Arm64MemoryBarrierOperation Value { get; private set; }
    }
}
