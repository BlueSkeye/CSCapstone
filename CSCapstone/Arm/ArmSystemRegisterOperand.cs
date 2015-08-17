using System;

namespace CSCapstone.Arm
{
    public sealed class ArmSystemRegisterOperand : ArmOperand
    {
        internal ArmSystemRegisterOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetEnum<ArmSysRegister>(from, ref offset);
            return;
        }

        public override ArmOperandType Type
        {
            get { return ArmOperandType.SysRegister; }
        }

        /// <summary>Get Operand's Register Value.</summary>
        /// <value>Retrieves the operand's register value if, and only if, the operand's
        /// type is <c>ArmInstructionOperandType.Register</c>. A null reference
        /// otherwise.</value>
        public ArmSysRegister Value { get; private set; }
    }
}
