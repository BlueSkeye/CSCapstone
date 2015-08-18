using System;

namespace CSCapstone.PowerPc
{
    public sealed class PowerPcRegisterOperand : PowerPcOperand
    {
        internal PowerPcRegisterOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetEnum<PowerPcRegister>(from, ref offset);
        }

        public override PowerPcOperandType Type
        {
            get { return PowerPcOperandType.PPC_OP_REG; }
        }

        public PowerPcRegister Value { get; private set; }
    }
}
