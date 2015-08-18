using System;

namespace CSCapstone.Sparc
{
    public sealed class SparcInstructionDetail
    {
        internal SparcInstructionDetail(IntPtr from, ref int offset)
        {
            ConditionCode = Helpers.GetEnum<SparcConditionCode>(from, ref offset);
            Hint = Helpers.GetEnum<SparcHint>(from, ref offset);
            byte operandsCount = Helpers.GetNativeByte(from, ref offset);
            if (8 < operandsCount) { throw new ApplicationException(); }
            Operands = new SparcOperand[operandsCount];
            for (int index = 0; index < operandsCount; index++) {
                Operands[index] = SparcOperand.Create(from, ref offset);
            }
        }

        public SparcConditionCode ConditionCode { get; private set; }

        public SparcHint Hint { get; private set; }

        public SparcOperand[] Operands { get; private set; }
    }
}
