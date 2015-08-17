using System;

namespace CSCapstone.Arm {
    /// <summary>ARM Instruction Detail.</summary>
    public sealed class ArmInstructionDetail {
        /// <summary>Create an ARM Instruction Detail.</summary>
        internal ArmInstructionDetail(IntPtr from, ref int offset)
        {
            LoadUserModeRegisters = Helpers.GetBoolean(from, ref offset);
            VectorSize = Helpers.GetNativeInt32(from, ref offset);
            VectorDataType = Helpers.GetEnum<ArmVectorDataType>(from, ref offset);
            CpsMode = Helpers.GetEnum<ArmCpsMode>(from, ref offset);
            CpsFlag = Helpers.GetEnum<ArmCpsFlag>(from, ref offset);
            CodeCondition = Helpers.GetEnum<ArmCodeCondition>(from, ref offset);
            UpdateFlags = Helpers.GetBoolean(from, ref offset);
            WriteBack = Helpers.GetBoolean(from, ref offset);
            MemoryBarrier = Helpers.GetEnum<ArmMemoryBarrier>(from, ref offset);
            byte operandsCount = Helpers.GetNativeByte(from, ref offset);
            if (36 < operandsCount) { throw new ApplicationException(); }
            Operands = new ArmOperand[operandsCount];
            for (int index = 0; index < operandsCount; index++) {
                Operands[index] = ArmOperand.Create(from, ref offset);
            }
            return;
        }
        
        /// <summary>Get Instruction's Code Condition.</summary>
        public ArmCodeCondition CodeCondition { get; private set; }

        /// <summary>Get Instruction's CPS Flag.</summary>
        public ArmCpsFlag CpsFlag { get; private set; }

        /// <summary>Get Instruction's CPS Mode.</summary>
        public ArmCpsMode CpsMode { get; private set; }

        /// <summary>Get Instruction's Load User Mode Registers Flag.</summary>
        public bool LoadUserModeRegisters { get; private set; }

        /// <summary>Get Instruction's Memory Barrier.</summary>
        public ArmMemoryBarrier MemoryBarrier { get; private set; }

        /// <summary>Get Instruction's Operands.</summary>
        public ArmOperand[] Operands { get; private set; }

        /// <summary>Get Instruction's Update Flags Flag.</summary>
        public bool UpdateFlags { get; private set; }

        /// <summary>Get Instruction's Vector Data Type.</summary>
        public ArmVectorDataType VectorDataType { get; private set; }

        /// <summary>Get Instruction's Vector Size.</summary>
        public int VectorSize { get; private set; }

        /// <summary>Get Instruction's Write Back Flag.</summary>
        public bool WriteBack { get; private set; }
    }
}