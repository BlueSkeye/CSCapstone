namespace CSCapstone {
    /// <summary>Dissembled Instruction.</summary>
    public sealed class Instruction<ArchInst, ArchReg, ArchGroup, ArchDetail>
    {
        /// <summary>Get Instruction's Address (EIP).</summary>
        public long Address { get; internal set; }

        /// <summary>Get Instruction's Architecture Dependent Detail.</summary>
        public ArchDetail ArchitectureDetail { get; internal set; }

        /// <summary>Get Instruction's Machine Bytes.</summary>
        public byte[] Bytes { get; internal set; }

        /// <summary>Get Instruction's Unique Identifier.</summary>
        public ArchInst Id { get; internal set; }

        /// <summary>Get Instruction's Architecture Independent Detail.</summary>
        public IndependentInstructionDetail<ArchReg, ArchGroup> IndependentDetail { get; internal set; }

        /// <summary>Get Instruction's Mnemonic Text.</summary>
        public string Mnemonic { get; internal set; }

        /// <summary>Get Instruction's Operand Text.</summary>
        public string Operand { get; internal set; }

        /// <summary>Create a Dissembled Instruction.</summary>
        internal Instruction()
        {
            return;
        }
    }
}