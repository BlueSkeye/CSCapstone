using System;

namespace CSCapstone.Arm {
    /// <summary>ARM Instruction Operand.</summary>
    public abstract class ArmOperand
    {
        /// <summary>Create an ARM Instruction Operand.</summary>
        protected ArmOperand(IntPtr from, ref int offset)
        {
            VectorIndex = Helpers.GetNativeInt32(from, ref offset);
            Shifter = Helpers.GetEnum<ArmShifter>(from, ref offset);
            ShifterValue = Helpers.GetNativeUInt32(from, ref offset);
            // Ignore operand type. We already discovered it.
            offset += sizeof(int);
            return;
        }

        /// <summary>Get Operand's Subtracted Flag.</summary>
        public bool IsSubtracted { get; internal set; }

        /// <summary>Get Operand's Shifter.</summary>
        public ArmShifter Shifter { get; internal set; }

        /// <summary>Get Operand's Shifter.</summary>
        public uint ShifterValue { get; internal set; }

        /// <summary>Get Operand's Type.</summary>
        public abstract ArmOperandType Type { get; }

        /// <summary>Get Operand's Vector Index.</summary>
        public int VectorIndex { get; internal set; }



        

        /// <summary>
        ///     Get Operand's SetEnd Value.
        /// </summary>
        /// <value>
        ///     Retrieves the operand's register value if, and only if, the operand's type is
        ///     <c>ArmInstructionOperandType.SetEnd</c>. A null reference otherwise.
        /// </value>
        public ArmSetEndOperandType? SetEndValue { get; internal set; }

        internal static ArmOperand Create(IntPtr from, ref int offset)
        {
            // TODO : Check for this offset being valid or both 32bits and 64bits
            // version ot the Capstone DLL.
            int initialOffset = offset;
            int operandTypeOffset = offset + OperandTypeNativeOffset;
            ArmOperand result;
            ArmOperandType operandType;
            switch (operandType = Helpers.GetEnum<ArmOperandType>(from, ref operandTypeOffset)) {
                case ArmOperandType.CImmediate:
                case ArmOperandType.Immediate:
                case ArmOperandType.PImmediate:
                    result = new ArmImmediateOperand(operandType, from, ref offset);
                    break;
                case ArmOperandType.FloatingPoint:
                    result = new ArmFloatingPointOperand(from, ref offset);
                    break;
                case ArmOperandType.Invalid:
                    return null;
                case ArmOperandType.Memory:
                    result = new ArmMemoryOperand(from, ref offset);
                    break;
                case ArmOperandType.Register:
                    result = new ArmRegisterOperand(from, ref offset);
                    break;
                case ArmOperandType.SetEnd:
                    result = new ArmSetEndOperand(from, ref offset);
                    break;
                case ArmOperandType.SysRegister:
                    result = new ArmSystemRegisterOperand(from, ref offset);
                    break;
                default:
                    throw new ApplicationException();
            }
            // Fix offset to reference first byte just after union.
            offset = initialOffset + OperandTypeNativeOffset + sizeof(int) + OperandsUnionSize;
            result.IsSubtracted = Helpers.GetBoolean(from, ref offset);
            return result;
        }

        private const int OperandTypeNativeOffset = 12;
        private const int OperandsUnionSize = 16;
    }
}