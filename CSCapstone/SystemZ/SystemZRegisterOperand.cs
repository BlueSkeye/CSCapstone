using System;

namespace CSCapstone.SystemZ
{
    public class SystemZRegisterOperand : SystemZOperand
    {
        public SystemZRegisterOperand(IntPtr from, ref int offset)
        {
            Value = Helpers.GetEnum<SystemZRegister>(from, ref offset);
        }

        public override SystemZOperandType Type
        {
            get { return SystemZOperandType.SYSZ_OP_REG; }
        }

        public SystemZRegister Value { get; private set; }
    }
}
