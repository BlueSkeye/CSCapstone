using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSCapstone
{
    /// <summary>A set of instruction details that are available for every
    /// architecture.</summary>
    /// <remarks>TODO : This class is not immutable. Array contents could be
    /// changed. Make this immutable.
    /// TOOD : Make this class abstract once we got rid of exiting instanciations</remarks>
    public /* abstract */ class InstructionDetailBase<ArchReg, ArchGroup>
    {
        /// <summary>Create an X86 Independent SupportedArchitecture Instruction Detail.</summary>
        internal InstructionDetailBase()
        {
            return;
        }

        // TODO : Make this abstract once the class will be. Also remove body.
        protected /* abstract */ ArchGroup ConvertGroup(byte value)
        {
            throw new NotImplementedException();
        }

        // TODO : Make this abstract once the class will be. Also remove body.
        protected /* abstract */ ArchReg ConvertRegister(byte value)
        {
            throw new NotImplementedException();
        }

        private delegate T ConvertDelegate<T>(byte value);

        protected InstructionDetailBase(IntPtr baseAddress, ref int offset)
        {
            // TODO : Check performances. This is a little bit heavy at runtime.
            ReadRegisters = ReadBlock<ArchReg>(12,
                delegate(byte value) { return ConvertRegister(value); },
                baseAddress, ref offset);
            WrittenRegisters = ReadBlock<ArchReg>(20,
                delegate(byte value) { return ConvertRegister(value); },
                baseAddress, ref offset);
            Groups = ReadBlock<ArchGroup>(8,
                delegate(byte value) { return ConvertGroup(value); },
                baseAddress, ref offset);
            return;
        }

        private T[] ReadBlock<T>(int maxBlockSize, ConvertDelegate<T> converter,
            IntPtr baseAddress, ref int offset)
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
                result[index] = converter(Helpers.GetNativeByte(baseAddress, ref offset));
            }
            offset += (1 + (maxBlockSize - itemsCount));
            return result;
        }
        
        /// <summary>Get Implicit Registers Read by an Instruction.</summary>
        public ArchReg[] ReadRegisters { get; internal set; }

        /// <summary>Get Implicit Registers Written by an Instruction.</summary>
        public ArchReg[] WrittenRegisters { get; internal set; }

        /// <summary>Get Groups an Instruction Belongs to.</summary>
        public ArchGroup[] Groups { get; internal set; }
    }
}