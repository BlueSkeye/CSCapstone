using System;

namespace CSCapstone.Arm64
{
    /// <summary>Instruction Detail.</summary>
    public sealed class Arm64InstructionDetail
    {
        /// <summary>Create an ARM64 Instruction Detail.</summary>
        internal Arm64InstructionDetail(
            Instruction<Arm64Mnemonic, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            CodeCondition = Helpers.GetEnum<Arm64CodeCondition>(from, ref offset);
            UpdateFlags = Helpers.GetBoolean(from, ref offset);
            WriteBack = Helpers.GetBoolean(from, ref offset);
            byte operandsCount = Helpers.GetNativeByte(from, ref offset);
            if (8 < operandsCount) { throw new ApplicationException(); }
            Operands = new Arm64Operand[operandsCount];
            for (int index = 0; index < operandsCount; index++) {
                Operands[index] = Arm64Operand.Create(instruction, from, ref offset);
            }
            return;
        }
        
        /// <summary>Get Instruction's Code Condition.</summary>
        public Arm64CodeCondition CodeCondition { get; private set; }

        /// <summary>Get Instruction's Operands.</summary>
        public Arm64Operand[] Operands { get; private set; }

        /// <summary>Get Instruction's Update Flags Flag.</summary>
        public bool UpdateFlags { get; private set; }

        /// <summary>Get Instruction's Write Back Flag.</summary>
        public bool WriteBack { get; private set; }
    }
}