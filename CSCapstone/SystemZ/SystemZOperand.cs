using System;

namespace CSCapstone.SystemZ
{
    public abstract class SystemZOperand
    {
        protected SystemZOperand()
        {
            return;
        }

        public abstract SystemZOperandType Type { get; }

        internal static SystemZOperand Create(IntPtr baseAddress, ref int offset)
        {
            SystemZOperand result;
            int initialOffset = offset;
            SystemZOperandType operandType = Helpers.GetEnum<SystemZOperandType>(baseAddress, ref offset);
            bool accessRegister = false;
            if (0 != (operandType & SystemZOperandType.SYSZ_OP_ACREG)) {
                accessRegister = true;
                operandType &= ~(SystemZOperandType.SYSZ_OP_ACREG);
            }
            switch (operandType) {
                case SystemZOperandType.SYSZ_OP_IMM:
                    result = new SystemZImmediateOperand(baseAddress, ref offset);
                    return null;
                case SystemZOperandType.SYSZ_OP_INVALID:
                    return null;
                case SystemZOperandType.SYSZ_OP_MEM:
                    result = new SystemZMemoryOperand(baseAddress, ref offset);
                    break;
                case SystemZOperandType.SYSZ_OP_REG:
                    result = new SystemZRegisterOperand(baseAddress, ref offset);
                    break;
                default:
                    throw new ApplicationException();
            }
            result.AccessRegister = accessRegister;
            offset = sizeof(int) + UnionSize;
            return result;
        }

        public bool AccessRegister { get; private set; }

        public const int UnionSize = 24;
    }
}
