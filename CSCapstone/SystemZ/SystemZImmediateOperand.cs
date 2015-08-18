using System;

namespace CSCapstone.SystemZ
{
    public sealed class SystemZImmediateOperand : SystemZOperand
    {
        internal SystemZImmediateOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetNativeInt64(from, ref offset);
        }

        public override SystemZOperandType Type
        {
            get { return SystemZOperandType.SYSZ_OP_IMM; }
        }

        public long Value { get; private set; }
    }
}
