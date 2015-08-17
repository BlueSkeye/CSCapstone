using System;

namespace CSCapstone.X86
{
    public sealed class X86ImmediateOperand : X86Operand
    {
        internal X86ImmediateOperand(IntPtr baseAddress, ref int offset)
        {
            Value = Helpers.GetNativeInt64(baseAddress, ref offset);
            return;
        }

        public override X86OperandType Type
        {
            get { return X86OperandType.Immediate; }
        }

        /// <summary>Get Operand's Immediate Value.</summary>
        /// <value>Retrieves the operand's immediate value if, and only if, the
        /// operand's type is <c>X86InstructionOperandType.Immediate</c>. In
        /// other words, <c>X86InstructionOperand.Type</c> is
        /// <c>X86InstructionOperandType.Immediate</c>. A null reference
        /// otherwise.</value>
        public long Value { get; private set; }
    }
}
