using System;

namespace CSCapstone.Sparc
{
    public sealed class SparcMemoryOperand : SparcOperand
    {
        internal SparcMemoryOperand(IntPtr from, ref int offset)
        {
            BaseRegister = Helpers.GetByteEnum<SparcRegister>(from, ref offset);
            IndexRegister = Helpers.GetByteEnum<SparcRegister>(from, ref offset);
            Displacement = Helpers.GetNativeInt32(from, ref offset);
        }

        public override SparcOperandType Type
        {
            get { return SparcOperandType.SPARC_OP_MEM; }
        }

        public SparcRegister BaseRegister { get; private set; }

        public int Displacement { get; private set; }

        public SparcRegister IndexRegister { get; private set; }
    }
}
