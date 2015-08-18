using System;

namespace CSCapstone.Mips
{
    public sealed class MipsMemoryOperand : MipsOperand
    {
        internal MipsMemoryOperand(IntPtr from, ref int offset)
        {
            Base = Helpers.GetNativeUInt32(from, ref offset);
            Displacement = Helpers.GetNativeInt64(from, ref offset);
        }

        public override MipsOperandType Type
        {
            get { return MipsOperandType.MIPS_OP_MEM; }
        }

        public uint Base { get; private set; }

        public long Displacement { get; private set; }
    }
}
