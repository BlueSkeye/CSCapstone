using System;

namespace CSCapstone.PowerPc
{
    public sealed class PowerPcMemoryOperand : PowerPcOperand
    {
        internal PowerPcMemoryOperand(IntPtr from, ref int offset)
        {
            Base = Helpers.GetNativeUInt32(from, ref offset);
            Displacement = Helpers.GetNativeInt32(from, ref offset);
        }

        public uint Base { get; private set; }

        public int Displacement { get; private set; }

        public override PowerPcOperandType Type
        {
            get { return PowerPcOperandType.PPC_OP_MEM; }
        }
    }
}
