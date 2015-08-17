using System;
using System.Runtime.InteropServices;
using CSCapstone.Arm;
using CSCapstone.Arm64;
using CSCapstone.X86;

namespace CSCapstone
{
    /// <summary>This class handles the architecture agnostic part of an instruction
    /// as returned by Capstone. This class is further extended by the templated
    /// class Instruction that is architecture specifi..</summary>
    [StructLayout(LayoutKind.Sequential)]
    public abstract class InstructionBase
    {
        protected InstructionBase(DisassemblerBase owner, IntPtr from, ref int offset)
        {
            if (null == owner) { throw new ArgumentNullException(); }
            _owner = owner;
            SetId(Helpers.GetNativeUInt32(from, ref offset));
            Address = Helpers.GetNativeUInt64(from, ref offset);
            CodeBytes = Helpers.GetNativeInlineBufferArray(from, 16,
                Helpers.GetNativeUInt16(from, ref offset), ref offset);
            // TODO : Check for the Intern use. Is it owrthy ?
            Mnemonic = string.Intern(Helpers.GetAnsiString(from, 32, ref offset));
            Operand = Helpers.GetAnsiString(from, 160, ref offset);
            return;
        }

        public ulong Address { get; private set; }
        
        /// <summary>Get Instruction's Managed Machine Bytes.</summary>
        /// <value>Convenient property to retrieve the instruction's machine bytes
        /// as a managed collection. The codeSize of the managed collection will always
        /// be equal to the value represented by <c>NativeInstruction.Size</c>.
        /// This property allocates managed memory for a new managed collection and
        /// uses direct memory copying tocopy the collection from unmanaged memory
        /// to managed memory every time it is invoked.</value>
        public byte[] CodeBytes { get; private set; }
        
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
        public string Mnemonic { get; private set; }

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
        public string Operand { get; private set; }

        public bool BelongsToGroup(uint groupId)
        {
            return _owner.BelongsToGroup(this, groupId);
        }

        /// <summary>Create a managed representation of a native instruction from
        /// the given native buffer.</summary>
        /// <param name="from">Native data to be decoded.</param>
        /// <returns>An instance of this class.</returns>
        internal static InstructionBase Create<Inst, Reg, Group, Detail>(
            Disassembler<Inst, Reg, Group, Detail> onBehalfOf, IntPtr from,
            out int size)
        {
            int offset = 0;
            try { return new Instruction<Inst, Reg, Group, Detail>(onBehalfOf, from, ref offset); }
            finally { size = Helpers.Align(sizeof(int), ref offset); }
        }

        protected abstract void SetId(uint value);

        /// <summary>Get Object's String Representation.</summary>
        /// <returns>The object's string representation.</returns>
        public override string ToString() {
            return String.Format("{0} {1}", this.Mnemonic, this.Operand);
        }

        private DisassemblerBase _owner;
    }
}
