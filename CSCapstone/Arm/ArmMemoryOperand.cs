using System;

namespace CSCapstone.Arm
{
    public class ArmMemoryOperand : ArmOperand
    {
        internal ArmMemoryOperand(IntPtr from, ref int offset)
            : base(from, ref offset)
        {
            BaseRegister = Helpers.GetNativeUInt32(from, ref offset);
            IndexRegister = Helpers.GetNativeUInt32(from, ref offset);
            IndexRegisterScale = Helpers.GetNativeInt32(from, ref offset);
            Displacement = Helpers.GetNativeInt32(from, ref offset);
            return;
        }

        /// <summary>Operand Value's BaseRegister Register.</summary>
        public uint BaseRegister { get; private set; }

        /// <summary>Operand Value's IndexRegister Register.</summary>
        public uint IndexRegister { get; private set; }

        /// <summary>Operand Value's IndexRegister Register Scale.</summary>
        public int IndexRegisterScale { get; private set; }

        /// <summary>Operand Value's Displacement Value.</summary>
        public int Displacement { get; private set; }
        
        public override ArmOperandType Type
        {
            get { return ArmOperandType.Memory; }
        }
    }
}
