using System;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>
    ///     Marshal Extension.
    /// </summary>
    internal static class MarshalExtension {
        /// <summary>
        ///     Allocate Memory For a Structure.
        /// </summary>
        /// <typeparam name="T">
        ///     The structure's type.
        /// </typeparam>
        /// <returns>
        ///     A pointer to the allocated memory.
        /// </returns>
        internal static IntPtr AllocHGlobal<T>() {
            var nType = MarshalExtension.SizeOf<T>();
            var pType = Marshal.AllocHGlobal(nType);

            return pType;
        }

        /// <summary>
        ///     Allocate Memory For a Structure.
        /// </summary>
        /// <param name="codeSize">
        ///     The collection's codeSize.
        /// </param>
        /// <typeparam name="T">
        ///     The structure's type.
        /// </typeparam>
        /// <returns>
        ///     A pointer to the allocated memory.
        /// </returns>
        internal static IntPtr AllocHGlobal<T>(int size) {
            var nType = MarshalExtension.SizeOf<T>() * size;
            var pType = Marshal.AllocHGlobal(nType);

            return pType;
        }

        /// <summary>
        ///     Marshal a Pointer to a Structure and Free Memory.
        /// </summary>
        /// <typeparam name="T">
        ///     The destination structure's type.
        /// </typeparam>
        /// <param name="p">
        ///     The pointer to marshal.
        /// </param>
        /// <returns>
        ///     The destination structure.
        /// </returns>
        internal static T FreePtrToStructure<T>(IntPtr p) {
            var @struct = Marshal.PtrToStructure(p, typeof (T));
            Marshal.FreeHGlobal(p);

            return (T) @struct;
        }

        /// <summary>
        ///     Marshal a Pointer to a Structure.
        /// </summary>
        /// <typeparam name="T">
        ///     The destination structure's type.
        /// </typeparam>
        /// <param name="p">
        ///     The pointer to marshal.
        /// </param>
        /// <returns>
        ///     The destination structure.
        /// </returns>
        internal static T PtrToStructure<T>(IntPtr p) {
            var @struct = Marshal.PtrToStructure(p, typeof (T));
            return (T) @struct;
        }

        /// <summary>
        ///     Get a Type's Size.
        /// </summary>
        /// <typeparam name="T">
        ///     The type.
        /// </typeparam>
        /// <returns>
        ///     The type's codeSize, in bytes.
        /// </returns>
        internal static int SizeOf<T>() {
            var size = Marshal.SizeOf(typeof (T));
            return size;
        }
    }
}