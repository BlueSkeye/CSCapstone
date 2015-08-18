using System;

namespace CSCapstone.Arm64
{
    public sealed class Arm64MemoryOperand : Arm64Operand
    {
        internal Arm64MemoryOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            BaseRegister = Helpers.GetEnum<Arm64Register>(from, ref offset);
            IndexRegister = Helpers.GetEnum<Arm64Register>(from, ref offset);
            Displacement = Helpers.GetNativeInt32(from, ref offset);
            return;
        }

        /// <summary>Memory operand base register.</summary>
        public Arm64Register BaseRegister { get; private set; }

        /// <summary>Memory operand base register.</summary>
        public int Displacement { get; private set; }

        /// <summary>Memory operand base register.</summary>
        public Arm64Register IndexRegister { get; private set; }

        public override Arm64OperandType Type
        {
            get { return Arm64OperandType.Memory; }
        }
    }
}
