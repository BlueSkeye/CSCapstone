using System;

namespace CSCapstone.X86
{
    /// <summary>X86 Instruction Memory Operand Value.</summary>
    public sealed class X86MemoryOperand : X86Operand
    {
        internal X86MemoryOperand(IntPtr from, ref int offset)
        {
            SegmentRegister = Helpers.GetEnum<X86Register>(from, ref offset);
            BaseRegister = Helpers.GetEnum<X86Register>(from, ref offset);
            IndexRegister = Helpers.GetEnum<X86Register>(from, ref offset);
            IndexRegisterScale = Helpers.GetNativeInt32(from, ref offset);
            Displacement = Helpers.GetNativeInt64(from, ref offset);
            return;
        }

        public override X86OperandType Type
        {
            get { return X86OperandType.Memory; }
        }

        /// <summary>Operand Value's Segment Register.</summary>
        public X86Register SegmentRegister { get; private set; }

        /// <summary>Operand Value's Base Register.</summary>
        public X86Register BaseRegister { get; private set; }

        /// <summary>Operand Value's Index Register.</summary>
        public X86Register IndexRegister { get; private set; }

        /// <summary>Operand Value's Index Register Scale.</summary>
        public int IndexRegisterScale { get; private set; }

        /// <summary>Operand Value's Displacement Value.</summary>
        public long Displacement { get; private set; }

        internal const int NativeSize = 24;
    }
}