using System;

namespace CSCapstone.X86
{
    public sealed class X86RegisterOperand : X86Operand
    {
        internal X86RegisterOperand(IntPtr baseAddress, ref int offset)
        {
            Value = Helpers.GetByteEnum<X86Register>(baseAddress, ref offset);
            return;
        }

        public override X86OperandType Type
        {
            get { return X86OperandType.Register; }
        }

        /// <summary>Get Operand's Register Value.</summary>
        /// <value>Retrieves the operand's register value if, and only if, the
        /// operand's type is <c>X86InstructionOperandType.Register</c>. In other
        /// words, <c>X86InstructionOperand.Type</c> is
        /// <c>X86InstructionOperandType.Register</c>. A null reference otherwise.
        /// </value>
        public X86Register Value { get; private set; }
    }
}
