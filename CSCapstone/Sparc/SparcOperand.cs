using System;

namespace CSCapstone.Sparc
{
    public abstract class SparcOperand
    {
        protected SparcOperand()
        {
            return;
        }

        public abstract SparcOperandType Type { get; }

        internal static SparcOperand Create(IntPtr baseAddress, ref int offset)
        {
            SparcOperand result;
            int initialOffset = offset;
            switch (Helpers.GetEnum<SparcOperandType>(baseAddress, ref offset)) {
                case SparcOperandType.SPARC_OP_IMM:
                    result = new SparcImmediateOperand(baseAddress, ref offset);
                    break;
                case SparcOperandType.SPARC_OP_INVALID:
                    return null;
                case SparcOperandType.SPARC_OP_MEM:
                    result = new SparcMemoryOperand(baseAddress, ref offset);
                    break;
                case SparcOperandType.SPARC_OP_REG:
                    result = new SparcRegisterOperand(baseAddress, ref offset);
                    break;
                default:
                    throw new ApplicationException();
            }
            offset = sizeof(int) + UnionSize;
            return result;
        }

        private const int UnionSize = 8;
    }
}
