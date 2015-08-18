using System;

namespace CSCapstone.XCore
{
    public sealed class XCoreMemoryOperand : XCoreOperand
    {
        internal XCoreMemoryOperand(IntPtr from, ref int offset)
        {
            BaseRegister = Helpers.GetByteEnum<XCoreRegister>(from, ref offset);
            IndexRegister = Helpers.GetByteEnum<XCoreRegister>(from, ref offset);
            Displacement = Helpers.GetNativeInt32(from, ref offset);
            Direction = Helpers.GetNativeInt32(from, ref offset);
        }

        public XCoreRegister BaseRegister { get; private set; }

        public int Direction { get; private set; }

        public int Displacement { get; private set; }

        public XCoreRegister IndexRegister { get; private set; }

        public override XCoreOperandType Type
        {
            get { return XCoreOperandType.XCORE_OP_MEM; }
        }
    }
}
