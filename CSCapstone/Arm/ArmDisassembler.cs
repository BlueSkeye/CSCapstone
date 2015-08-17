namespace CSCapstone.Arm
{
    /// <summary>Capstone ARM Disassembler.</summary>
    public sealed class ArmDisassembler : Disassembler<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail>
    {
        /// <summary>Create a Capstone ARM Disassembler.</summary>
        /// <param name="mode">The disassembler's mode.</param>
        public ArmDisassembler(SupportedMode mode)
            : base(SupportedArchitecture.Arm, mode)
        {
            return;
        }

        internal override ArmInstructionDetail CreateDetail(System.IntPtr from, ref int offset)
        {
            throw new System.NotImplementedException();
        }

        protected override Instruction<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail> CreateInstruction(System.IntPtr nativeInstruction)
        {
            throw new System.NotImplementedException();
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
        //protected override Instruction<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail> CreateInstruction(InstructionBase nativeInstruction) {
        //    var @object = nativeInstruction.AsArmInstruction();

        //    // Get Native Instruction's Managed Independent Detail.
        //    //
        //    // Retrieves the native instruction's managed independent detail once to avoid having to allocate
        //    // new memory every time it is retrieved.
        //    var nativeIndependentInstructionDetail = nativeInstruction.ManagedIndependentDetail;
        //    if (nativeIndependentInstructionDetail != null) {
        //        @object.ArchitectureDetail = nativeInstruction.NativeArmDetail.AsArmInstructionDetail();
        //        @object.IndependentDetail = nativeIndependentInstructionDetail.Value.AsArmIndependentInstructionDetail();
        //    }

        //    return @object;
        //}
    }
}