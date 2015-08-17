using System;

namespace CSCapstone {
    /// <summary>This class extends the <see cref="InstructionBase"/> class and deals
    /// with the architecture dependant part of the instruction.</summary>
    public sealed class Instruction<Mnemonic, Reg, Group, Detail>
        : InstructionBase
    {
        /// <summary>Create a Dissembled Instruction.</summary>
        internal Instruction(Disassembler<Mnemonic, Reg, Group, Detail> owner, IntPtr from, ref int offset)
            : base(owner, from, ref offset)
        {
            if (!DisassemblerBase.DietModeEnabled) {
                // TODO : Check performances. This is a little bit heavy at runtime.
                ReadRegisters = ReadBlock<Reg>(12, from, ref offset);
                WrittenRegisters = ReadBlock<Reg>(20, from, ref offset);
                Groups = ReadBlock<Group>(8, from, ref offset);
                ArchitectureDetail = owner.CreateDetail(from, ref offset);
            }
            return;
        }
        
        /// <summary>Get Instruction's SupportedArchitecture Dependent Detail.</summary>
        public Detail ArchitectureDetail { get; internal set; }

        /// <summary>Get Instruction's Unique Identifier.</summary>
        public Mnemonic InstructionId
        {
            get { return _instructionId; }
        }

        /// <summary>Get Implicit Registers Read by an Instruction.</summary>
        public Reg[] ReadRegisters { get; internal set; }

        /// <summary>Get Implicit Registers Written by an Instruction.</summary>
        public Reg[] WrittenRegisters { get; internal set; }

        /// <summary>Get Groups an Instruction Belongs to.</summary>
        public Group[] Groups { get; internal set; }

        private T[] ReadBlock<T>(int maxBlockSize, IntPtr baseAddress, ref int offset)
        {
            int savedOffset = 0;
            int itemsCount;

            try {
                savedOffset = offset;
                itemsCount = Helpers.GetNativeByte(baseAddress, ref offset);
            }
            finally { offset = savedOffset; }
            if (itemsCount > maxBlockSize) { throw new ApplicationException(); }
            T[] result = new T[itemsCount];
            for (int index = 0; index < itemsCount; index++) {
                result[index] = Helpers.GetByteEnum<T>(baseAddress, ref offset);
            }
            offset += (1 + (maxBlockSize - itemsCount));
            return result;
        }

        protected override void SetId(uint value)
        {
            _instructionId = (Mnemonic)((IConvertible)value).ToType(typeof(Mnemonic), null);
        }

        private Mnemonic _instructionId;
    }
}