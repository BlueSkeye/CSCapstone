using System;

namespace CSCapstone.PowerPc
{
    public sealed class PowerPcImmediateOperand : PowerPcOperand
    {
        internal PowerPcImmediateOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetNativeInt32(from, ref offset);
        }

        public override PowerPcOperandType Type
        {
            get { return PowerPcOperandType.PPC_OP_IMM; }
        }

        public int Value { get; private set; }
    }
}
