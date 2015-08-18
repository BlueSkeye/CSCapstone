using System;

namespace CSCapstone.XCore
{
    public abstract class XCoreOperand
    {
        protected XCoreOperand()
        {
            return;
        }

        public abstract XCoreOperandType Type { get; }

        internal static XCoreOperand Create(IntPtr baseAddress, ref int offset)
        {
            XCoreOperand result;
            int initialOffset = offset;
            switch (Helpers.GetEnum<XCoreOperandType>(baseAddress, ref offset)) {
                case XCoreOperandType.XCORE_OP_IMM:
                    result = new XCoreImmediateOperand(baseAddress, ref offset);
                    break;
                case XCoreOperandType.XCORE_OP_INVALID:
                    return null;
                case XCoreOperandType.XCORE_OP_MEM:
                    result = new XCoreMemoryOperand(baseAddress, ref offset);
                    break;
                case XCoreOperandType.XCORE_OP_REG:
                    result = new XCoreRegisterOperand(baseAddress, ref offset);
                    break;
                default:
                    throw new ApplicationException();
            }
            offset = sizeof(int) + UnionSize;
            return result;
        }

        public const int UnionSize = 12;
    }
}
