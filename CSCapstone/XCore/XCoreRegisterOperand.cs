using System;

namespace CSCapstone.XCore
{
    public sealed class XCoreRegisterOperand : XCoreOperand
    {
        internal XCoreRegisterOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetEnum<XCoreRegister>(from, ref offset);
        }

        public override XCoreOperandType Type
        {
            get { return XCoreOperandType.XCORE_OP_REG; }
        }

        public XCoreRegister Value { get; private set; }
    }
}
