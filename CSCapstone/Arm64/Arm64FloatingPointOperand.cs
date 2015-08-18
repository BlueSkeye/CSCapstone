using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64FloatingPointOperand : Arm64Operand
    {
        internal Arm64FloatingPointOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            Value = Helpers.GetNativeDouble(from, ref offset);
            return;
        }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.FloatingPoint; }
        }

        /// <summary>Get Operand's Immediate Value.</summary>
        /// <value>Retrieves the operand's immediate value if, and only if, the
        /// operand's type is either <c>ArmInstructionOperandType.CImmediate</c>,
        /// <c>ArmInstructionOperandType.Immediate</c>, or
        /// <c>ArmInstructionOperandType.PImmediate</c>. A null reference
        /// otherwise.</value>
        public double Value { get; private set; }
    }
}
