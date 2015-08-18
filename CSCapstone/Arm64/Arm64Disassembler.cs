using System;

namespace CSCapstone.Arm64
{
    /// <summary>Capstone ARM64 Disassembler.</summary>
    public sealed class Arm64Disassembler : Disassembler<Arm64Mnemonic, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail>
    {
        /// <summary>Create a Capstone ARM64 Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public Arm64Disassembler(SupportedMode mode)
            : base(SupportedArchitecture.Arm64, mode)
        {
            return;
        }

        internal override Arm64InstructionDetail CreateDetail(
            Instruction<Arm64Mnemonic, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            return new Arm64InstructionDetail(instruction, from, ref offset);
        }

        protected override Instruction<Arm64Mnemonic, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail> CreateInstruction(System.IntPtr nativeInstruction)
        {
            int offset = 0;
            return new Instruction<Arm64Mnemonic, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail>(this, nativeInstruction, ref offset);
        }

        /// <summary>
        ///     Create a Dissembled Instruction.
        /// </summary>
        /// <param name="nativeInstruction">
        ///     A native instruction.
        /// </param>
        /// <returns>
        ///     A dissembled instruction.
        /// </returns>
        //protected override Instruction<Arm64Instruction, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail> CreateInstruction(InstructionBase nativeInstruction) {
        //    var @object = nativeInstruction.AsArm64Instruction();

        //    // Get Native Instruction's Managed Independent Detail.
        //    //
        //    // Retrieves the native instruction's managed independent detail once to avoid having to allocate
        //    // new memory every time it is retrieved.
        //    var nativeIndependentInstructionDetail = nativeInstruction.ManagedIndependentDetail;
        //    if (nativeIndependentInstructionDetail != null) {
        //        @object.ArchitectureDetail = nativeInstruction.NativeArm64Detail.AsArm64InstructionDetail(@object.InstructionId);
        //        @object.IndependentDetail = nativeIndependentInstructionDetail.Value.AsArm64IndependentInstructionDetail();
        //    }

        //    return @object;
        //}
    }
}