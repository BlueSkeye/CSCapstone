using System;

namespace CSCapstone.SystemZ
{
    public sealed class SystemZInstructionDetail
    {
        internal SystemZInstructionDetail(IntPtr from, ref int offset)
        {
            CodeCondition = Helpers.GetEnum<SystemZCodeCondition>(from, ref offset);
            byte operandsCount = Helpers.GetNativeByte(from, ref offset);
            if (8 < operandsCount) { throw new ApplicationException(); }
            Operands = new SystemZOperand[operandsCount];
            for (int index = 0; index < operandsCount; index++) {
                Operands[index] = SystemZOperand.Create(from, ref offset);
            }
        }

        public SystemZCodeCondition CodeCondition { get; private set; }

        public SystemZOperand[] Operands { get; private set; }
    }
}
