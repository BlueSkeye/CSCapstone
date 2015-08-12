using CSCapstone.Arm;
using CSCapstone.Arm64;
using CSCapstone.X86;

namespace CSCapstone
{
    /// <summary>Native Instruction Extension.</summary>
    public static class NativeInstructionExtension 
    {
        /// <summary>Convert a Native Instruction to a Dissembled Instruction.</summary>
        /// <param name="this">A native instruction.</param>
        /// <returns>A dissembled instruction.</returns>
        public static Instruction<ArchInst, ArchReg, ArchGroup, ArchDetail> AsInstruction<ArchInst, ArchReg, ArchGroup, ArchDetail>(this NativeInstruction @this)
            where ArchInst : struct, System.IConvertible
        {
            return new Instruction<ArchInst, ArchReg, ArchGroup, ArchDetail>() {
                Address = (long)@this.Address,
                Bytes = @this.ManagedBytes,
                Mnemonic = @this.ManagedMnemonic,
                Operand = @this.ManagedOperand,
                Id = (ArchInst)(System.Enum.ToObject(typeof(ArchInst), @this.InstructionId))
            };
        }

        /// <summary>Create an ARM Dissembled Instruction.</summary>
        /// <param name="this">A native instruction.</param>
        /// <returns>A dissembled instruction.</returns>
        public static Instruction<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail> AsArmInstruction(this NativeInstruction @this)
        {
            var @object = @this.AsInstruction<ArmInstruction, ArmRegister, ArmInstructionGroup, ArmInstructionDetail>();
            @object.Id = (ArmInstruction) @this.InstructionId;

            return @object;
        }

        /// <summary>Convert a Native Instruction to a ARM64 Dissembled Instruction.</summary>
        /// <param name="this">A native instruction.</param>
        /// <returns>A dissembled instruction.</returns>
        public static Instruction<Arm64Instruction, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail> AsArm64Instruction(this NativeInstruction @this)
        {
            var @object = @this.AsInstruction<Arm64Instruction, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail>();
            @object.Id = (Arm64Instruction) @this.InstructionId;

            return @object;
        }

        /// <summary>Convert a Native Instruction to an X86 Dissembled Instruction.</summary>
        /// <param name="this">A native instruction.</param>
        /// <returns>A dissembled instruction.</returns>
        public static Instruction<X86Instruction, X86Register, X86InstructionGroup, X86InstructionDetail> AsX86Instruction(this NativeInstruction @this)
        {
            var @object = @this.AsInstruction<X86Instruction, X86Register, X86InstructionGroup, X86InstructionDetail>();
            @object.Id = (X86Instruction) @this.InstructionId;

            return @object;
        }
    }
}