using System;

namespace CSCapstone.X86
{
    /// <summary>X86 Instruction Operand.</summary>
    public abstract class X86Operand
    {
        protected X86Operand()
        {
            return;
        }

        /// <summary>Get Operand's AVX Broadcast.</summary>
        public X86AvxBroadcast AvxBroadcast { get; internal set; }

        /// <summary>Get Operand's AVX Zero Operation Mask Flag.</summary>
        public bool AvxZeroOperationMask { get; internal set; }

        /// <summary>size of this operand (in bytes).</summary>
        public int Size { get; internal set; }

        public abstract X86OperandType Type { get; }

        internal static X86Operand Create(IntPtr baseAddress, ref int offset)
        {
            X86OperandType operandType =
                Helpers.GetByteEnum<X86OperandType>(baseAddress, ref offset);
            X86Operand result;
            int beforeUnionOffset = offset;
            switch (operandType) {
                case X86OperandType.FloatingPoint:
                    result = new X86FloatingPointOperand(baseAddress, ref offset);
                    break;
                case X86OperandType.Immediate:
                    result = new X86ImmediateOperand(baseAddress, ref offset);
                    break;
                case X86OperandType.Invalid:
                    return null;
                case X86OperandType.Memory:
                    result = new X86MemoryOperand(baseAddress, ref offset);
                    break;
                case X86OperandType.Register:
                    result = new X86RegisterOperand(baseAddress, ref offset);
                    break;
                default:
                    throw new ApplicationException();
            }
            // Manuallly ix offset to account for the union, whichecver member of
            // it has been acquired by the extension class.
            offset = beforeUnionOffset + X86MemoryOperand.NativeSize;
            // Finalize operand.
            result.Size = Helpers.GetNativeByte(baseAddress, ref offset);
            result.AvxBroadcast = Helpers.GetEnum<X86AvxBroadcast>(baseAddress, ref offset);
            result.AvxZeroOperationMask = Helpers.GetBoolean(baseAddress, ref offset);
            return result;
        }
    }
}