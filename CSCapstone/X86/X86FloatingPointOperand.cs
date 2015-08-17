using System;

namespace CSCapstone.X86
{
    public sealed class X86FloatingPointOperand : X86Operand
    {
        internal X86FloatingPointOperand(IntPtr baseAddress, ref int offset)
        {
            Value = Helpers.GetNativeDouble(baseAddress, ref offset);
            return;
        }

        public override X86OperandType Type
        {
            get { return X86OperandType.FloatingPoint; }
        }

        /// <summary>Get Operand's Floating Point Value.</summary>
        /// <value>Retrieves the operand's floating point value if, and only if,
        /// the operand's type is <c>X86InstructionOperandType.FloatingPoint</c>.
        /// In other words, <c>X86InstructionOperand.Type</c> is
        /// <c>X86InstructionOperandType.FloatingPoint</c>. A null reference
        /// otherwise.</value>
        public double Value { get; private set; }
    }
}
