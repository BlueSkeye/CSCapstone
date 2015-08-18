using System;

namespace CSCapstone.PowerPc
{
    public sealed class PowerPcInstructionDetail
    {
        internal PowerPcInstructionDetail(IntPtr from, ref int offset)
        {
            BranchCode = Helpers.GetEnum<PowerPcBranchCode>(from, ref offset);
            BranchHint = Helpers.GetEnum<PowerPcBranchHint>(from, ref offset);
            UpdateCR0 = Helpers.GetBoolean(from, ref offset);

            byte operandsCount = Helpers.GetNativeByte(from, ref offset);
            if (8 < operandsCount) { throw new ApplicationException(); }
            Operands = new PowerPcOperand[operandsCount];
            for (int index = 0; index < operandsCount; index++) {
                Operands[index] = PowerPcOperand.Create(from, ref offset);
            }
        }

        public PowerPcBranchCode BranchCode { get; private set; }

        public PowerPcBranchHint BranchHint { get; private set; }

        public PowerPcOperand[] Operands { get; private set; }

        public bool UpdateCR0 { get; private set; }
    }
}
