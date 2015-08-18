using System;

namespace CSCapstone.Arm64 {
    /// <summary>ARM64 Instruction Operand.</summary>
    public abstract class Arm64Operand
    {
        protected Arm64Operand(IntPtr from, ref int offset)
        {
            VectorIndex = Helpers.GetNativeInt32(from, ref offset);
            VectorArrangementSpecifier = Helpers.GetEnum<Arm64VectorArrangementSpecifier>(from, ref offset);
            VectorElementSizeSpecifier = Helpers.GetEnum<Arm64VectorElementSizeSpecifier>(from, ref offset);
            Shifter = Helpers.GetEnum<Arm64Shifter>(from, ref offset);
            ShifterValue = Helpers.GetNativeUInt32(from, ref offset);
            Extender = Helpers.GetEnum<Arm64Extender>(from, ref offset);
            // Ignore operand type. We already discovered it.
            offset += sizeof(int);
            // The following union will be decode by estension classes. Having a
            // 64 bits value inside the union requires alignment on a 64 bits
            // boundary.
            offset = Helpers.Align(sizeof(long), ref offset);
            return;
        }

        /// <summary>Get Operand's Extender.</summary>
        public Arm64Extender Extender { get; private set; }

        public bool IsSubtracted { get; private set; }

        /// <summary>Get Operand's Shifter.</summary>
        public Arm64Shifter Shifter { get; private set; }

        /// <summary>Get Operand's Shifter Value.</summary>
        public uint ShifterValue { get; private set; }

        /// <summary>Get Operand's Type.</summary>
        public abstract Arm64OperandType Type { get; }

        /// <summary>Operand's Vector Arrangement Specifier.</summary>
        public Arm64VectorArrangementSpecifier VectorArrangementSpecifier { get; private set; }

        /// <summary>Get Operand's Vector Element Size Specifier.</summary>
        public Arm64VectorElementSizeSpecifier VectorElementSizeSpecifier { get; private set; }

        /// <summary>Get Operand's Vector IndexRegister.</summary>
        public int VectorIndex { get; private set; }

        internal static Arm64Operand Create(
            Instruction<Arm64Mnemonic, Arm64Register, Arm64InstructionGroup, Arm64InstructionDetail> instruction,
            IntPtr from, ref int offset)
        {
            // TODO : Check for this offset being valid on both 32bits and 64bits
            // version ot the Capstone DLL.
            int initialOffset = offset;
            int operandTypeOffset = offset + OperandTypeNativeOffset;
            Arm64Operand result;
            Arm64OperandType operandType;
            switch (operandType = Helpers.GetEnum<Arm64OperandType>(from, ref operandTypeOffset))
            {
                case Arm64OperandType.CImmediate:
                case Arm64OperandType.Immediate:
                    result = new Arm64ImmediateOperand(operandType, from, ref offset);
                    break;
                case Arm64OperandType.FloatingPoint:
                    result = new Arm64FloatingPointOperand(from, ref offset);
                    break;
                case Arm64OperandType.Invalid:
                    return null;
                case Arm64OperandType.Memory:
                    result = new Arm64MemoryOperand(from, ref offset);
                    break;
                case Arm64OperandType.MemoryBarrierOperation:
                    result = new Arm64MemoryBarrierOperand(from, ref offset);
                    break;
                case Arm64OperandType.MrsRegister:
                    result = new Arm64MrsRegisterOperand(from, ref offset);
                    break;
                case Arm64OperandType.MsrRegister:
                    result = new Arm64MsrRegisterOperand(from, ref offset);
                    break;
                case Arm64OperandType.PrefetchOperation:
                    result = new Arm64PrefetchOperand(from, ref offset);
                    break;
                case Arm64OperandType.PState:
                    result = new Arm64PStateOperand(from, ref offset);
                    break;
                case Arm64OperandType.Register:
                    result = new Arm64RegisterOperand(from, ref offset);
                    break;
                case Arm64OperandType.SysOperation:
            		// unsigned int sys;  // IC/DC/AT/TLBI operation (see arm64_ic_op, arm64_dc_op, arm64_at_op, arm64_tlbi_op)
                    switch(instruction.InstructionId) {
                        case Arm64Mnemonic.AT:
                            result = new Arm64AtOperand(from, ref offset);
                            break;
                        case Arm64Mnemonic.IC:
                            result = new Arm64IcOperand(from, ref offset);
                            break;
                        case Arm64Mnemonic.DC:
                            result = new Arm64DcOperand(from, ref offset);
                            break;
                        case Arm64Mnemonic.TLBI:
                            result = new Arm64TlbiOperand(from, ref offset);
                            break;
                        default:
                            throw new ApplicationException();
                    }
                    break;
                default:
                    throw new ApplicationException();
            }
            // Fix offset to reference first byte just after union.
            offset = initialOffset + OperandTypeNativeOffset + sizeof(int) + OperandsUnionSize;
            result.IsSubtracted = Helpers.GetBoolean(from, ref offset);
            return result;
        }

        /// <summary>Get Operand's AT Instruction Operation.</summary>
        /// <value>Retrieves the operand's AT instruction operation if, and only if, the operand's
        /// type is <c>Arm64InstructionOperandType.SysOperation</c> and the instruction the operand applies to is
        ///     <c>Arm64Instruction.AT</c>. A null reference otherwise.
        /// </value>
        public Arm64AtOperation? AtInstructionOperation { get; internal set; }

        /// <summary>
        ///     Get Operand's DC Instruction Operation.
        /// </summary>
        /// <value>
        ///     Retrieves the operand's DC instruction operation if, and only if, the operand's type is
        ///     <c>Arm64InstructionOperandType.SysOperation</c> and the instruction the operand applies to is
        ///     <c>Arm64Instruction.DC</c>. A null reference otherwise.
        /// </value>
        public Arm64DcOperation? DcInstructionOperation { get; internal set; }

        /// <summary>
        ///     Get Operand's IC Instruction Operation.
        /// </summary>
        /// <value>
        ///     Retrieves the operand's IC instruction operation if, and only if, the operand's type is
        ///     <c>Arm64InstructionOperandType.SysOperation</c> and the instruction the operand applies to is
        ///     <c>Arm64Instruction.IC</c>. A null reference otherwise.
        /// </value>
        public Arm64IcOperation? IcInstructionOperation { get; internal set; }

        /// <summary>
        ///     Get Operand's TLBI Instruction Operation.
        /// </summary>
        /// <value>
        ///     Retrieves the operand's TLBI instruction operation if, and only if, the operand's type is
        ///     <c>Arm64InstructionOperandType.SysOperation</c> and the instruction the operand applies to is
        ///     <c>Arm64Instruction.TLBI</c>. A null reference otherwise.
        /// </value>
        public Arm64TlbiOperation? TlbiInstructionOperation { get; internal set; }

        private const int OperandTypeNativeOffset = 24;
        private const int OperandsUnionSize = 12;
    }
}