using System;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>
    ///     Native Capstone Disassembler.
    /// </summary>
    public static class NativeCapstone {
        /// <summary>
        ///     Disassemble Binary Code.
        /// </summary>
        /// <param name="handle">
        ///     A Capstone handle. Should not be a null reference.
        /// </param>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <param name="count">
        ///     The number of instructions to disassemble. A 0 indicates all instructions should be disassembled.
        /// </param>
        /// <param name="startingAddress">
        ///     The address of the first instruction in the collection of bytes to disassemble.
        /// </param>
        /// <returns>
        ///     A native instruction handle.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public static SafeNativeInstructionHandle Disassemble(SafeCapstoneHandle handle, byte[] code, int count, long startingAddress) {
            // Copy Code to Unmanaged Memory.
            //
            // ...
            var pCode = MarshalExtension.AllocHGlobal<byte>(code.Length);
            Marshal.Copy(code, 0, pCode, code.Length);

            var pCount = (IntPtr) count;
            var pHandle = handle.DangerousGetHandle();
            var pInstructions = IntPtr.Zero;
            var pSize = (IntPtr) code.Length;
            var uStartingAddress = (ulong) startingAddress;

            // Disassemble Binary Code.
            //
            // ...
            var pResultCode = CapstoneImport.Disassemble(pHandle, pCode, pSize, uStartingAddress, pCount, ref pInstructions);
            if (pResultCode == IntPtr.Zero) {
                throw new InvalidOperationException("Unable to disassemble binary code.");
            }

            var iResultCode = (int) pResultCode;
            var instructions = MarshalExtension.PtrToStructure<NativeInstruction>(pInstructions, iResultCode);

            // Free Unmanaged Memory.
            //
            // Avoid a memory leak.
            Marshal.FreeHGlobal(pCode);

            var instructionHandle = new SafeNativeInstructionHandle(instructions, pInstructions, pResultCode);
            return instructionHandle;
        }

        /// <summary>
        ///     Disassemble Binary Code.
        /// </summary>
        /// <remarks>
        ///     Convenient method to disassemble binary code with the assumption that the address of the first
        ///     instruction in the collection of bytes to disassemble is 0x1000. Equivalent to calling
        ///     <c>NativeCapstone.Disassemble(handle, code, 0x1000, count)</c>.
        /// </remarks>
        /// <param name="handle">
        ///     A Capstone handle. Should not be a null reference.
        /// </param>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <param name="count">
        ///     The number of instructions to disassemble. A 0 indicates all instructions should be disassembled.
        /// </param>
        /// <returns>
        ///     A native instruction handle.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public static SafeNativeInstructionHandle Disassemble(SafeCapstoneHandle handle, byte[] code, int count) {
            var instructionHandle = NativeCapstone.Disassemble(handle, code, 0x1000, count);
            return instructionHandle;
        }

        /// <summary>
        ///     Disassemble All Binary Code.
        /// </summary>
        /// <param name="handle">
        ///     A Capstone handle. Should not be a null reference.
        /// </param>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <param name="startingAddress">
        ///     The address of the first instruction in the collection of bytes to disassemble.
        /// </param>
        /// <returns>
        ///     A native instruction handle.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public static SafeNativeInstructionHandle DisassembleAll(SafeCapstoneHandle handle, byte[] code, long startingAddress) {
            var instructionHandle = NativeCapstone.Disassemble(handle, code, 0, startingAddress);
            return instructionHandle;
        }

        /// <summary>
        ///     Disassemble All Binary Code.
        /// </summary>
        /// <remarks>
        ///     Convenient method to disassemble binary code with the assumption that the address of the first
        ///     instruction in the collection of bytes to disassemble is 0x1000. Equivalent to calling
        ///     <c>NativeCapstone.DisassembleAll(handle, code, 0x1000)</c>.
        /// </remarks>
        /// <param name="handle">
        ///     A Capstone handle. Should not be a null reference.
        /// </param>
        /// <param name="code">
        ///     A collection of bytes representing the binary code to disassemble. Should not be a null reference.
        /// </param>
        /// <returns>
        ///     A native instruction handle.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if the binary code could not be disassembled.
        /// </exception>
        public static SafeNativeInstructionHandle DisassembleAll(SafeCapstoneHandle handle, byte[] code) {
            var instructions = NativeCapstone.DisassembleAll(handle, code, 0x1000);
            return instructions;
        }
    }
}