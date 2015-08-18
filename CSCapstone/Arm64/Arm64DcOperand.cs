using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64DcOperand : Arm64Operand
    {
        internal Arm64DcOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<Arm64DcOperation>(from, ref offset);
        }

        /// <summary></summary>
        /// <remarks>We don't have distinct identifiers for the various system
        /// operations despite they are implemented in separate classes.</remarks>
        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.SysOperation; }
        }

        public Arm64DcOperation Value { get; private set; }
    }
}
