using System;

namespace CSCapstone.SystemZ
{
    public sealed class SystemZMemoryOperand : SystemZOperand
    {
        internal SystemZMemoryOperand(IntPtr from, ref int offset)
        {
            BaseRegister = Helpers.GetByteEnum<SystemZRegister>(from, ref offset);
            IndexRegister = Helpers.GetByteEnum<SystemZRegister>(from, ref offset);
            Length = Helpers.GetNativeUInt64(from, ref offset);
            Displacement = Helpers.GetNativeUInt64(from, ref offset);
        }

        public override SystemZOperandType Type
        {
            get { return SystemZOperandType.SYSZ_OP_MEM; }
        }

        public SystemZRegister BaseRegister { get; private set; }

        public ulong Displacement { get; private set; }

        public SystemZRegister IndexRegister { get; private set; }

        public ulong Length { get; private set; }
    }
}
