using System;

namespace CSCapstone.XCore
{
    public sealed class XCoreImmediateOperand : XCoreOperand
    {
        internal XCoreImmediateOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetNativeInt32(from, ref offset);
        }

        public override XCoreOperandType Type
        {
            get { return XCoreOperandType.XCORE_OP_IMM; }
        }

        public int Value { get; private set; }
    }
}
