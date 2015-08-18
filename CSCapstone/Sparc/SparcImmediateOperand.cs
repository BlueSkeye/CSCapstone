using System;

namespace CSCapstone.Sparc
{
    public sealed class SparcImmediateOperand : SparcOperand
    {
        internal SparcImmediateOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetNativeInt32(from, ref offset);
        }

        public override SparcOperandType Type
        {
            get { return SparcOperandType.SPARC_OP_IMM; }
        }

        public int Value { get; private set; }
    }
}
