using System;

namespace CSCapstone.PowerPc
{
    public abstract class PowerPcOperand
    {
        protected PowerPcOperand()
        {
            return;
        }

        public abstract PowerPcOperandType Type { get; }

        internal static PowerPcOperand Create(IntPtr baseAddress, ref int offset)
        {
            PowerPcOperand result;
            int initialOffset = offset;
            switch (Helpers.GetEnum<PowerPcOperandType>(baseAddress, ref offset)) {
                case PowerPcOperandType.PPC_OP_CRX:
                    result = new PowerPcConditionRegisterOperand(baseAddress, ref offset);
                    break;
                case PowerPcOperandType.PPC_OP_IMM:
                    result = new PowerPcImmediateOperand(baseAddress, ref offset);
                    break;
                case PowerPcOperandType.PPC_OP_INVALID:
                    return null;
                case PowerPcOperandType.PPC_OP_MEM:
                    throw new NotImplementedException();
                case PowerPcOperandType.PPC_OP_REG:
                    result = new PowerPcRegisterOperand(baseAddress, ref offset);
                    break;
                default:
                    throw new ApplicationException();
            }
            offset = sizeof(int) + UnionSize;
            return result;
        }

        private const int UnionSize = 12;
    }
}
