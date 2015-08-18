using System;

namespace CSCapstone.PowerPc
{
    public sealed class PowerPcConditionRegisterOperand : PowerPcOperand
    {
        internal PowerPcConditionRegisterOperand(IntPtr from, ref int offset)
        {
            Scale = Helpers.GetNativeUInt32(from, ref offset);
            Register = Helpers.GetEnum<PowerPcRegister>(from, ref offset);
            BranchCode = Helpers.GetEnum<PowerPcBranchCode>(from, ref offset);
        }

        public override PowerPcOperandType Type
        {
            get { return PowerPcOperandType.PPC_OP_CRX; }
        }

        public PowerPcBranchCode BranchCode { get; private set; }

        public PowerPcRegister Register { get; private set; }

        public uint Scale { get; private set; }
    }
}
