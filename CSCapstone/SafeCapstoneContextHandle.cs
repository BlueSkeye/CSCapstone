using System;
using System.Runtime.InteropServices;

namespace CSCapstone
{
    public class SafeCapstoneContextHandle : SafeCapstoneHandle
    {
        protected SafeCapstoneContextHandle(IntPtr handle)
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
            SafeCapstoneContextHandle mySelf = this;
            return (CapstoneErrorCode.Ok == CapstoneImport.Close(ref mySelf));
        }
        
        internal class Marshaler : ICustomMarshaler
        {
            public void CleanUpManagedData(object ManagedObj)
            {
                throw new NotImplementedException();
            }

            public void CleanUpNativeData(IntPtr pNativeData)
            {
                throw new NotImplementedException();
            }

            public int GetNativeDataSize()
            {
                throw new NotImplementedException();
            }

            public IntPtr MarshalManagedToNative(object ManagedObj)
            {
                throw new NotImplementedException();
            }

            public object MarshalNativeToManaged(IntPtr pNativeData)
            {
                throw new NotImplementedException();
            }
        }
    }
}
