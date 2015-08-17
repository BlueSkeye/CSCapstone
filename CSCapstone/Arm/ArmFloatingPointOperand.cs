using System;

namespace CSCapstone.Arm
{
    public sealed class ArmFloatingPointOperand : ArmOperand
    {
        internal ArmFloatingPointOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetNativeDouble(from, ref offset);
            return;
        }

        public override ArmOperandType Type
        {
            get { return ArmOperandType.FloatingPoint; }
        }
    
        /// <summary>Get Operand's Floating Point Value.</summary>
        /// <value>Retrieves the operand's floating point value if, and only if, the
        /// operand's type is <c>ArmInstructionOperandType.FloatingPoint</c>. A null
        /// reference otherwise.</value>
        public double Value { get; private set; }
    }
}
