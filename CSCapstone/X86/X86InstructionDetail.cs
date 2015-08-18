using System;
using System.Runtime.InteropServices;

namespace CSCapstone.X86 {
    /// <summary>X86 Instruction Detail.</summary>
    public sealed class X86InstructionDetail
    {
        /// <summary>Create an X86 Instruction Detail.</summary>
        internal X86InstructionDetail(IntPtr from, ref int offset)
        {
            Prefix = Helpers.GetByteEnum<X86Prefix>(from, ref offset, 4);
            OperationCode = Helpers.GetBytes(from, ref offset, 4);
            Rex = Helpers.GetNativeByte(from, ref offset);
            AddressSize = Helpers.GetNativeByte(from, ref offset);
            ModRm = Helpers.GetNativeByte(from, ref offset);
            Sib = Helpers.GetNativeByte(from, ref offset);
            Displacement = Helpers.GetNativeInt32(from, ref offset);
            SibIndexRegister = Helpers.GetEnum<X86Register>(from, ref offset);
            SibScale = Helpers.GetNativeByte(from, ref offset);
            SibBaseRegister = Helpers.GetEnum<X86Register>(from, ref offset);
            SseCodeCondition = Helpers.GetEnum<X86SSECodeCondition>(from, ref offset);
            AvxCodeCondition = Helpers.GetEnum<X86AvxCodeCondition>(from, ref offset);
            SuppressAllAvxExceptions = Helpers.GetBoolean(from, ref offset);
            AvxRoundingMode = Helpers.GetEnum<X86AvxRoundingMode>(from, ref offset);
            byte operandsCount = Helpers.GetNativeByte(from, ref offset);
            if (8 < operandsCount) { throw new ApplicationException(); }
            Operands = new X86Operand[operandsCount];
            for (int index = 0; index < operandsCount; index++) {
                Operands[index] = X86Operand.Create(from, ref offset);
            }
            return;
        }
        
        /// <summary>Get Instruction's Prefix.</summary>
        public X86Prefix[] Prefix { get; private set; }

        /// <summary>Get Instruction's Operation Code.</summary>
        public byte[] OperationCode { get; private set; }

        /// <summary>Get Instruction's REX Prefix.</summary>
        public byte Rex { get; private set; }

        /// <summary>Get Instruction's Address Size.</summary>
        public byte AddressSize { get; private set; }

        /// <summary>Get Instruction's ModR/M Byte.</summary>
        public byte ModRm { get; private set; }

        /// <summary>Get Instruction's SIB Value.</summary>
        public byte Sib { get; private set; }

        /// <summary>Get Instruction's Displacement Value.</summary>
        public int Displacement { get; private set; }

        /// <summary>Get Instruction's SIB IndexRegister Register.</summary>
        public X86Register SibIndexRegister { get; private set; }

        /// <summary>Instruction's SIB Scale.</summary>
        public byte SibScale { get; private set; }

        /// <summary>Get Instruction's SIB BaseRegister Register.</summary>
        public X86Register SibBaseRegister { get; private set; }

        /// <summary>Get Instruction's SSE Code Condition.</summary>
        public X86SSECodeCondition SseCodeCondition { get; private set; }

        /// <summary>Get Instruction's Managed AVX Code Condition.</summary>
        public X86AvxCodeCondition AvxCodeCondition { get; private set; }

        /// <summary>Instruction's Suppress All AVX Exceptions Flag.</summary>
        public bool SuppressAllAvxExceptions { get; private set; }

        /// <summary>Get Instruction's Managed AVX Rounding Mode.</summary>
        public X86AvxRoundingMode AvxRoundingMode { get; private set; }

        public X86Operand[] Operands { get; private set; }
    }
}