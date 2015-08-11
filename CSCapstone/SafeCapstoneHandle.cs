using System;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>Safe handles us the recommended way for memory leaks avoidance when
    /// programming against the .Net framework when invoking native functions. This
    /// abstract base class will be the parent of every Capstone related object that
    /// is transiting between the Capstone native library and the C# wrapper library.
    /// Currently there is two linds of handles : <see cref="SafeCapstoneContextHandle"/>
    /// and <see cref="SafeCapstoneInstructionHandle"/>
    /// </summary>
    public abstract class SafeCapstoneHandle : SafeHandle {
        /// <summary>Capstone library returns a NULL value for invalid pointers.
        /// </summary>
        /// <param name="handle">The native handle to be wrapped by this class.</param>
        public SafeCapstoneHandle(IntPtr handle)
            : base(IntPtr.Zero, true)
        {
            base.handle = handle;
            return;
        }

        public override bool IsInvalid
        {
            get { return IntPtr.Zero == base.handle; }
        }
    }
}