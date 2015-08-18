using System;

namespace CSCapstone.Mips
{
    public sealed class MipsInstructionDetail
    {
        internal MipsInstructionDetail(IntPtr from, ref int offset)
        {
            byte operandsCount = Helpers.GetNativeByte(from, ref offset);
            if (8 < operandsCount) { throw new ApplicationException(); }
            Operands = new MipsOperand[operandsCount];
            for (int index = 0; index < operandsCount; index++) {
                Operands[index] = MipsOperand.Create(from, ref offset);
            }
        }

        public MipsOperand[] Operands { get; private set; }
    }
}
