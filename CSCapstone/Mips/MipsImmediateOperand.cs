using System;

namespace CSCapstone.Mips
{
    public sealed class MipsImmediateOperand : MipsOperand
    {
        internal MipsImmediateOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetNativeInt64(from, ref offset);
        }

        public override MipsOperandType Type
        {
            get { return MipsOperandType.MIPS_OP_IMM; }
        }

        public long Value { get; private set; }
    }
}
