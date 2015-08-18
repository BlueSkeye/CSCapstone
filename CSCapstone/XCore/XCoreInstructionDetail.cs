using System;

namespace CSCapstone.XCore
{
    public sealed class XCoreInstructionDetail
    {
        internal XCoreInstructionDetail(IntPtr from, ref int offset)
        {
            Operand = XCoreOperand.Create(from, ref offset);
        }

        public XCoreOperand Operand { get; private set; }
    }
}
