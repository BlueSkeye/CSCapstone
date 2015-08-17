using System;

namespace CSCapstone.Arm
{
    public sealed class ArmImmediateOperand : ArmOperand
    {
        internal ArmImmediateOperand(ArmOperandType operandType, IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            _operandType = operandType;
            Value = Helpers.GetNativeInt32(from, ref offset);
            return;
        }

        public override ArmOperandType Type
        {
            get { return _operandType; }
        }

        /// <summary>Get Operand's Immediate Value.</summary>
        /// <value>Retrieves the operand's immediate value if, and only if, the
        /// operand's type is either <c>ArmInstructionOperandType.CImmediate</c>,
        /// <c>ArmInstructionOperandType.Immediate</c>, or
        /// <c>ArmInstructionOperandType.PImmediate</c>. A null reference
        /// otherwise.</value>
        public int Value { get; private set; }

        private ArmOperandType _operandType;
    }
}
