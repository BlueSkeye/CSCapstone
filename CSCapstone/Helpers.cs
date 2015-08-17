using System;
using System.Runtime.InteropServices;

namespace CSCapstone
{
    /// <summary>A collection of helper functions.</summary>
    internal static class Helpers
    {
        internal static int Align(int boundarySize, ref int offset)
        {
            int delta;

            if (0 != (delta = offset % boundarySize)) {
                offset += (boundarySize - delta);
            }
            return offset;
        }

        internal static string GetAnsiString(IntPtr baseAddress, uint inlineSize, ref int offset)
        {
            try { return Marshal.PtrToStringAnsi(baseAddress + offset, (int)inlineSize).TrimGarbageAtEnd(); }
            finally { offset += (int)inlineSize; }
        }

        internal static bool GetBoolean(IntPtr baseAddress, ref int offset)
        {
            try { return (0 != Marshal.ReadByte(baseAddress, offset)); }
            finally { offset += 1; }
        }

        internal static T[] GetByteEnum<T>(IntPtr baseAddress, ref int offset, int count)
        {
            T[] result = new T[count];
            for(int index = 0; index < count; index++) {
                result[index] = GetByteEnum<T>(baseAddress, ref offset);
            }
            return result;
        }

        internal static T GetByteEnum<T>(IntPtr baseAddress, ref int offset)
        {
            return (T)((IConvertible)Helpers.GetNativeByte(baseAddress, ref offset)).ToType(typeof(T), null);
        }

        internal static byte[] GetBytes(IntPtr baseAddress, ref int offset, int count)
        {
            byte[] result = new byte[count];
            Marshal.Copy(baseAddress + offset, result, 0, count);
            offset += count;
            return result;
        }

        internal static T GetEnum<T>(IntPtr baseAddress, ref int offset)
        {
            return (T)((IConvertible)Helpers.GetNativeInt32(baseAddress, ref offset)).ToType(typeof(T), null);
        }

        internal static byte GetNativeByte(IntPtr baseAddress, ref int offset)
        {
            try { return Marshal.ReadByte(baseAddress, offset); }
            finally { offset += 1; }
        }

        internal static double GetNativeDouble(IntPtr baseAddress, ref int offset)
        {
            try {
                Align(sizeof(double), ref offset);
                return (double)(Marshal.PtrToStructure(baseAddress, typeof(double)));
            }
            finally { offset += sizeof(double); }
        }

        internal static byte[] GetNativeInlineBufferArray(IntPtr baseAddress, uint inlineSize,
            uint usedSize, ref int offset)
        {
            if (usedSize > inlineSize) { throw new ArgumentException(); }
            byte[] result = new byte[usedSize];
            Marshal.Copy(baseAddress + offset, result, 0, (int)usedSize);
            offset += (int)inlineSize;
            return result;
        }

        internal static IntPtr GetNativeIntPtr(IntPtr baseAddress, ref int offset)
        {
            try { return Marshal.ReadIntPtr(baseAddress, Align(IntPtr.Size, ref offset)); }
            finally { offset += IntPtr.Size; }
        }

        internal static int GetNativeInt32(IntPtr baseAddress, ref int offset)
        {
            try { return Marshal.ReadInt32(baseAddress, Align(sizeof(int), ref offset)); }
            finally { offset += sizeof(int); }
        }

        internal static long GetNativeInt64(IntPtr baseAddress, ref int offset)
        {
            try { return Marshal.ReadInt64(baseAddress, Align(sizeof(long), ref offset)); }
            finally { offset += sizeof(long); }
        }

        internal static ushort GetNativeUInt16(IntPtr baseAddress, ref int offset)
        {
            try { return (ushort)Marshal.ReadInt16(baseAddress, Align(sizeof(ushort), ref offset)); }
            finally { offset += sizeof(ushort); }
        }

        internal static uint GetNativeUInt32(IntPtr baseAddress, ref int offset)
        {
            try { return (uint)Marshal.ReadInt32(baseAddress, Align(sizeof(uint), ref offset)); }
            finally { offset += sizeof(uint); }
        }

        internal static ulong GetNativeUInt64(IntPtr baseAddress, ref int offset)
        {
            try { return (ulong)Marshal.ReadInt64(baseAddress, Align(sizeof(ulong), ref offset)); }
            finally { offset += sizeof(ulong); }
        }

        internal static string TrimGarbageAtEnd(this string from)
        {
            int index = from.IndexOf('\0');
            switch (index) {
                case -1:
                    return from;
                case 0:
                    return string.Empty;
                default:
                    return from.Substring(0, index);
            }
        }

        /// <summary>Interpret the native Capstone error code and throw an
        /// appropriate error if the value is anything else than CapstoneErrorCode.Ok
        /// </summary>
        /// <param name="lastError">The error code to be checked.</param>
        internal static void ThrowOnCapstoneError(this CapstoneErrorCode lastError)
        {
            switch (lastError) {
                case CapstoneErrorCode.Ok:
                    return;
                case CapstoneErrorCode.OutOfMemory:
                    throw new OutOfMemoryException("Out of memory.");
                case CapstoneErrorCode.UnsupportedArchitecture:
                    throw new ArgumentException("Unsupported architecture.");
                case CapstoneErrorCode.InvalidHandle:
                    throw new ArgumentException("Invalid handle.");
                case CapstoneErrorCode.InvalidCapstone:
                    throw new ArgumentException("Invalid handle.");
                case CapstoneErrorCode.UnsupportedMode:
                    throw new ArgumentException("Unsupported mode.");
                case CapstoneErrorCode.UnsupportedOption:
                    throw new ArgumentException("Unsupported option.");
                case CapstoneErrorCode.UnavailableInstructionDetail:
                    throw new InvalidOperationException("Unavailable disassemble instruction detail.");
                case CapstoneErrorCode.UninitializedDynamicMemoryManagement:
                    throw new InvalidOperationException("Uninitialized dynamic memory management.");
                default:
                    throw new InvalidOperationException(
                        string.Format("Unexpected Capstone return code 0x{0:X8}", lastError));
            }
        }
    }
}
