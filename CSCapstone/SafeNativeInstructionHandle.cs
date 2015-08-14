using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;

namespace CSCapstone {
    /// <summary>Safe Native Instruction Handle.</summary>
    /// <remarks>Represents a safe handle to a collection of native instructions
    /// copied from unmanaged memory to managed memory. A managed native instruction
    /// has a pointer to unmanaged memory that has to be freed after the instruction
    /// has been processed by application code to avoid memory leaks. A safe handle
    /// ensures that memory is eventually freed automatically if application code
    /// fails to free it explicitly.</remarks>
    public sealed class SafeNativeInstructionHandle : SafeCapstoneHandle
    {
        public SafeNativeInstructionHandle()
            : base(true)
        {
            this._instructionCount = (IntPtr)1;
            return;
        }

        internal SafeNativeInstructionHandle(IntPtr handle)
            : base(handle)
        {
            return;
        }

        /// <summary>Create a Safe Native Instruction Handle.</summary>
        /// <param name="instructions">A collection of instructions. Should not be a
        /// null reference.</param>
        /// <param name="instructions">A pointer to disassembled instructions in
        /// unmanaged memory.</param>
        /// <param name="instructionCount">A platform specific integer representing
        /// the number of disassembled instructions in unmanaged memory.</param>
        public SafeNativeInstructionHandle(IEnumerable<NativeInstruction> instructions,
            IntPtr pInstructions, IntPtr instructionCount)
            : base(true)
        {
            this._instructions = instructions;
            this.handle = pInstructions;
            this._instructionCount = instructionCount;
        }

        /// <summary>Get Instructions.</summary>
        public IEnumerable<NativeInstruction> Instructions {
            get {
                return this._instructions;
            }
        }

        /// <summary>Get Instruction Count.</summary>
        /// <value>A platform specific integer representing the number of disassembled
        /// instructions in unmanaged memory.</value>
        internal IntPtr InstructionCount {
            get {
                return this._instructionCount;
            }
        }

        /// <summary>Get Instruction Pointer.</summary>
        /// <value>A pointer to disassembled instructions in unmanaged memory.
        /// </value>
        internal IntPtr InstructionPointer {
            get {
                return this.handle;
            }
        }

        /// <summary>Release Handle.</summary>
        /// <returns>A boolean true if the handle was released. A boolean false
        /// otherwise.</returns>
        protected override bool ReleaseHandle()
        {
            lock (ReleaseLock) {
                try {
                    _beingFreed = this;
                    CapstoneImport.Free(this, this._instructionCount);
                }
                finally { _beingFreed = null; }
            }
            this._instructions = Enumerable.Empty<NativeInstruction>();
            return true;
        }
    
        /// <summary>Instruction Count.</summary>
        private readonly IntPtr _instructionCount;
        /// <summary>Instructions.</summary>
        private IEnumerable<NativeInstruction> _instructions;
        private static readonly object ReleaseLock = new object();
        private static SafeNativeInstructionHandle _beingFreed;

        internal class Marshaler : ICustomMarshaler
        {
            private Marshaler(bool enforceFreingLock)
            {
                _enforecFreeingLock = enforceFreingLock;
            }

            public void CleanUpManagedData(object ManagedObj)
            {
                throw new NotImplementedException();
            }

            public void CleanUpNativeData(IntPtr pNativeData)
            {
                throw new NotImplementedException();
            }

            public static ICustomMarshaler GetInstance(string cookie)
            {
                if (FreeMarshalerCookie == cookie) {
                    return _freeMarshalerSingleton;
                }
                throw new ArgumentException();
            }

            public int GetNativeDataSize()
            {
                throw new NotImplementedException();
            }

            public IntPtr MarshalManagedToNative(object ManagedObj)
            {
                SafeNativeInstructionHandle instructionHandle = (SafeNativeInstructionHandle)ManagedObj;
                if (null == instructionHandle) { return IntPtr.Zero; }
                
                if (_enforecFreeingLock) {
                    if (SafeNativeInstructionHandle._beingFreed != instructionHandle) {
                        throw new InvalidOperationException();
                    }
                }
                return instructionHandle.handle;
            }

            public object MarshalNativeToManaged(IntPtr pNativeData)
            {
                throw new NotImplementedException();
            }

            private static readonly Marshaler _freeMarshalerSingleton = new Marshaler(true);
            internal const string FreeMarshalerCookie = "FREE";
            private bool _enforecFreeingLock;
        }
    }
}