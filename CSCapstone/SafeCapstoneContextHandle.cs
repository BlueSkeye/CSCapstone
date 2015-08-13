using System;
using System.Runtime.InteropServices;

namespace CSCapstone
{
    public class SafeCapstoneContextHandle : SafeCapstoneHandle
    {
        public SafeCapstoneContextHandle()
            : base(true)
        {
            return;
        }

        public SafeCapstoneContextHandle(IntPtr handle)
            : base(handle)
        {
            return;
        }

        /// <summary>This is a required override. The method invokes the Capstone
        /// provided native function that will release the handle.</summary>
        /// <returns>True on successfull handle release, false otherwise.</returns>
        /// <remarks>TODO : if the handle is not properly released by the native
        /// library, we should consider throwing an exception instead of returning
        /// a boolean. This is almost certainly an unex^pected condition that require
        /// some kind of fix.</remarks>
        protected override bool ReleaseHandle()
        {
            // We must use a local variable in order to be able to use the ref modifier
            // on Close call.
            SafeCapstoneContextHandle mySelf = this;
            if (IntPtr.Zero == mySelf.handle) { throw new InvalidOperationException(); }
            CapstoneImport.Close(ref mySelf).ThrowOnCapstoneError();
            // We need to reset the handle by ourselves.
            this.handle = IntPtr.Zero;
            return true;
        }
        
        internal class RefMarshaler : ICustomMarshaler
        {
            /// <summary>Nothing to do really because the MarshalNativeToManaged
            /// didn't allocated any data.</summary>
            /// <param name="ManagedObj"></param>
            public void CleanUpManagedData(object ManagedObj)
            {
                // Nothing to do here. The managed object is the one that has been
                // marshaled in. However we want it to continue living.
                return;
            }

            public void CleanUpNativeData(IntPtr pNativeData)
            {
                throw new NotImplementedException("CleanUpNativeData");
            }

            public static ICustomMarshaler GetInstance(string cookie)
            {
                return _singleton;
            }

            public int GetNativeDataSize()
            {
                throw new NotImplementedException("GetNativeDataSize");
            }

            public IntPtr MarshalManagedToNative(object ManagedObj)
            {
                if (null == ManagedObj) { return IntPtr.Zero; }
                return ((SafeCapstoneContextHandle)ManagedObj).handle;
            }

            public object MarshalNativeToManaged(IntPtr pNativeData)
            {
                throw new NotImplementedException("MarshalNativeToManaged");
            }

            private static readonly RefMarshaler _singleton = new RefMarshaler();
        }
    }
}
