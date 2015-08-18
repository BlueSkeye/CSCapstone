using System;

namespace CSCapstone.Mips
{
    public abstract class MipsOperand
    {
        protected MipsOperand()
        {
            return;
        }

        public abstract MipsOperandType Type { get; }

        internal static MipsOperand Create(IntPtr baseAddress, ref int offset)
        {
            MipsOperand result;
            int initialOffset = offset;
            switch (Helpers.GetEnum<MipsOperandType>(baseAddress, ref offset)) {
                case MipsOperandType.MIPS_OP_IMM:
                    result = new MipsImmediateOperand(baseAddress, ref offset);
                    break;
                case MipsOperandType.MIPS_OP_INVALID:
                    return null;
                case MipsOperandType.MIPS_OP_MEM:
                    throw new NotImplementedException();
                case MipsOperandType.MIPS_OP_REG:
                    result = new MipsRegisterOperand(baseAddress, ref offset);
                    break;
                default:
                    throw new ApplicationException();
            }
            offset = initialOffset + UnionSize;
            return result;
        }

        private const int UnionSize = 16;
    }
}
