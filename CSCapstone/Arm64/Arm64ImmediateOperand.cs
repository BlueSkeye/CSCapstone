using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64ImmediateOperand : Arm64Operand
    {
        internal Arm64ImmediateOperand(Arm64OperandType operandType, IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            _operandType = operandType;
            Value = Helpers.GetNativeInt64(from, ref offset);
            return;
        }

        public override Arm64OperandType Type
        {
            get { return _operandType; }
        }

        /// <summary>Get Operand's Immediate Value.</summary>
        /// <value>Retrieves the operand's immediate value if, and only if, the
        /// operand's type is either <c>ArmInstructionOperandType.CImmediate</c>,
        /// <c>ArmInstructionOperandType.Immediate</c>, or
        /// <c>ArmInstructionOperandType.PImmediate</c>. A null reference
        /// otherwise.</value>
        public long Value { get; private set; }

        private Arm64OperandType _operandType;
    }
}
