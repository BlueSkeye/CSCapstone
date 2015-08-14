using System;

namespace CSCapstone {
    /// <summary>This class extends the <see cref="InstructionBase"/> class and deals
    /// with the architecture dependant part of the instruction.</summary>
    public sealed class Instruction<Inst, Reg, Group, Detail>
        : InstructionBase
    {
        /// <summary>Get Instruction's SupportedArchitecture Dependent Detail.</summary>
        public Detail ArchitectureDetail { get; internal set; }

        /// <summary>Get Instruction's Unique Identifier.</summary>
        public new Inst InstructionId
        {
            get {
                // TODO
                throw new NotImplementedException();
                // return (Inst)base.InstructionId;
            }
        }

        /// <summary>Get Instruction's SupportedArchitecture Independent Detail.</summary>
        public InstructionDetailBase<Reg, Group> IndependentDetail { get; internal set; }

        /// <summary>Get Instruction's Mnemonic Text.</summary>
        // public string Mnemonic { get; internal set; }

        /// <summary>Create a Dissembled Instruction.</summary>
        internal Instruction(Disassembler<Inst, Reg, Group, Detail> owner, IntPtr from, ref int offset)
            : base(owner, from, ref offset)
        {
            return;
        }
    }
}