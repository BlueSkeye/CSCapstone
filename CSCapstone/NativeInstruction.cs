using System;
using System.Runtime.InteropServices;
using CSCapstone.Arm;
using CSCapstone.Arm64;
using CSCapstone.X86;

namespace CSCapstone
{
    /// <summary>Native Instruction.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeInstruction
    {
        public ulong Address { get; set; }
        public uint InstructionId { get; set; }
        /// <summary>Get Instruction's Managed Machine Bytes.</summary>
        /// <value>Convenient property to retrieve the instruction's machine bytes
        /// as a managed collection. The codeSize of the managed collection will always
        /// be equal to the value represented by <c>NativeInstruction.Size</c>.
        /// This property allocates managed memory for a new managed collection and
        /// uses direct memory copying tocopy the collection from unmanaged memory
        /// to managed memory every time it is invoked.</value>
        public byte[] ManagedBytes { get; private set; }
        /// <summary>Get Instruction's Managed Mnemonic Text.</summary>
        /// <value>Convenient property to retrieve the instruction's mnemonic ASCII
        /// text as a managed string. This property allocates managed memory for a
        /// new managed string every time it is invoked.</value>
        /// <exception cref="System.ArgumentException">Thrown if a managed string
        /// could not be initialized.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the
        /// unmanaged string is too large to allocate in managed memory.</exception>
        /// <exception cref="System.AccessViolationException">Thrown if the unmanaged
        /// string is inaccessible.</exception>
        public string ManagedMnemonic { get; private set; }
        /// <summary>Get Instruction's Managed Operand Text.</summary>
        /// <value>Convenient property to retrieve the instruction's operand ASCII
        /// text as a managed string. This property allocates managed memory for a
        /// new managed string every time it is invoked.</value>
        /// <exception cref="System.ArgumentException">Thrown if a managed string
        /// could not be initialized.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the
        /// unmanaged string is too large to allocate in managed memory.</exception>
        /// <exception cref="System.AccessViolationException">Thrown if the unmanaged
        /// string is inaccessible.</exception>
        public string ManagedOperand { get; private set; }
        // public ushort Size { get; set; }

        /// <summary>Get Instruction's Managed Architecture Independent Detail.</summary>
        /// <value>Convenient property to retrieve the instruction's architecture
        /// independent detail as a managed structure. This property allocates managed
        /// memory for a new managed structure every time it is invoked. If <c>
        /// NativeInstruction.IndependentDetail</c> is equal to <c>IntPtr.Zero</c>, a
        /// null reference is returned. If the unmanaged structure is freed or is
        /// inaccessible, the return value is undefined.</value>
        public NativeIndependentInstructionDetail? ManagedIndependentDetail {
            get {
                NativeIndependentInstructionDetail? managedDetail = null;
                if (this.IndependentDetail != IntPtr.Zero) {
                    managedDetail = MarshalExtension.PtrToStructure<NativeIndependentInstructionDetail>(this.IndependentDetail);
                }
                return managedDetail;
            }
        }

        /// <summary>Get Instruction's ARM64 Detail.</summary>
        public NativeArm64InstructionDetail NativeArm64Detail {
            get {
                var pDetail = CapstoneProxyImport.Arm64Detail(this.IndependentDetail);
                var detail = MarshalExtension.PtrToStructure<NativeArm64InstructionDetail>(pDetail);

                return detail;
            }
        }

        /// <summary>Get Instruction's ARM Detail.</summary>
        public NativeArmInstructionDetail NativeArmDetail {
            get {
                var pDetail = CapstoneProxyImport.ArmDetail(this.IndependentDetail);
                var detail = MarshalExtension.PtrToStructure<NativeArmInstructionDetail>(pDetail);

                return detail;
            }
        }

        /// <summary>Get Instruction's X86 Detail.</summary>
        public NativeX86InstructionDetail NativeX86Detail {
            get {
                var pDetail = CapstoneProxyImport.ArmDetail(this.IndependentDetail);
                var detail = MarshalExtension.PtrToStructure<NativeX86InstructionDetail>(pDetail);

                return detail;
            }
        }

        /// <summary>Create a managed representation of a native instruction from
        /// the given native buffer.</summary>
        /// <param name="from">Native data to be decoded.</param>
        /// <returns>An instance of this class.</returns>
        internal static NativeInstruction Create<Inst, Reg, Group, Detail>(
            CapstoneDisassembler<Inst, Reg, Group, Detail> onBehalfOf, IntPtr from)
        {
            int offset = 0;
            // WARNING : Do not change properties initialization order. They match
            // the native structure field order. Order of parameters in some Helpers
            // functions is also very sensitive.
            NativeInstruction result = new NativeInstruction() {
                InstructionId = Helpers.GetNativeUInt32(from, ref offset),
                Address = Helpers.GetNativeUInt64(from, ref offset),
                ManagedBytes = Helpers.GetNativeInlineBufferArray(from, 16, Helpers.GetNativeUInt16(from, ref offset), ref offset),
                ManagedMnemonic = Helpers.GetAnsiString(from, 32, ref offset),
                ManagedOperand = Helpers.GetAnsiString(from, 160, ref offset),
            };
            IntPtr nativeDetails = Helpers.GetNativeIntPtr(from, ref offset);
            // TODO : Handle details.
            return result;
        }

        /// <summary>Get Object's String Representation.</summary>
        /// <returns>The object's string representation.</returns>
        public override string ToString() {
            return String.Format("{0} {1}", this.ManagedMnemonic, this.ManagedOperand);
        }
    
        /// <summary>Instruction ID (basically a numeric ID for the instruction mnemonic)
        /// Find the instruction id in the '[ARCH]_insn' enum in the header file  of
        /// corresponding architecture, such as 'arm_insn' in arm.h for ARM, 'x86_insn'
        /// in x86.h for X86, etc... This information is available even when CS_OPT_DETAIL =
        /// CS_OPT_OFF
        /// NOTE: in Skipdata mode, "data" instruction has 0 for this id field.</summary>
        // unsigned int Id;
        /// <summary>Address (EIP) of this instruction. This information is available
        /// even when CS_OPT_DETAIL = CS_OPT_OFF.</summary>
        // uint64_t Address;
        /// <summary>Size of this instruction. This information is available even
        /// when CS_OPT_DETAIL = CS_OPT_OFF.</summary>
        // uint16_t Size;
        /// <summary>Machine bytes of this instruction, with number of bytes indicated
        /// by @codeSize above This information is available even when CS_OPT_DETAIL =
        /// CS_OPT_OFF.</summary>
        // public fixed byte Bytes[16];
        /// <summary>Ascii text of instruction mnemonic. This information is available
        /// even when CS_OPT_DETAIL = CS_OPT_OFF</summary>
        // public fixed byte Mnemonic[32];
        /// <summary>Ascii text of instruction operands. This information is available
        /// even when CS_OPT_DETAIL = CS_OPT_OFF.</summary>
        // public fixed byte Operand[160];
        /// <summary>Pointer to cs_detail. NOTE: detail pointer is only valid when
        /// both requirements below are met:
        /// (1) CS_OP_DETAIL = CS_OPT_ON
        /// (2) Engine is not in Skipdata mode (CS_OP_SKIPDATA option set to CS_OPT_ON)
        /// NOTE 2: when in Skipdata mode, or when detail mode is OFF, even if this
        /// pointer is not NULL, its content is still irrelevant.</summary>
        public IntPtr /* cs_detail * */ IndependentDetail;
    }
}
