using System;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>
    ///     Capstone Import.
    /// </summary>
    public static class CapstoneImport
    {
        /// <summary>Close a Capstone Handle.</summary>
        /// <param name="pHandle">A pointer to a Capstone handle.</param>
        /// <returns>An integer indicating the result of the operation.</returns>
        /// <remarks>We MUST implement our own marshaler otherwise the standard 
        /// marshaler will attempt some tricks to preserve reference counting the
        /// safe handle. Those tricks would interfere with the internal state of
        /// the safe handle and would trigger an "Handle already closed exception."
        /// Additionally, the ref modifier is required because the native function
        /// expects a pointer to the handle and not the handle itself.</remarks>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_close")]
        internal static extern CapstoneErrorCode Close(
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SafeCapstoneContextHandle.RefMarshaler))] ref SafeCapstoneContextHandle pHandle);

        /// <summary>Disassemble Binary Code.</summary>
        /// <param name="pHandle">A <see cref="SafeCapstoneContextHandle"/> as retrieved
        /// by a call to <see cref="Open"/>.</param>
        /// <param name="pCode">An array of bytes holding the code to be disassembled.</param>
        /// <param name="codeSize">A platform specific integer representing the number
        /// of instructions to disassemble.
        /// TODO : Instructions or buytes ?</param>
        /// <param name="startingAddress">The address of the first instruction in
        /// the collection of bytes to disassemble.</param>
        /// <param name="count">A platform specific integer representing the number of
        /// instructions to disassemble. A <c>IntPtr.Zero</c> indicates all instructions
        /// should be disassembled.</param>
        /// <param name="instruction">On return this parameter will be updated with a
        /// pointer at an array of A pointer to a collection of disassembled instructions.
        /// </param>
        /// <returns>A platform specific integer representing the number of instructions
        /// disassembled. An <c>IntPtr.Zero</c> indicates no instructions were disassembled
        /// as a result of an error.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_disasm")]
        public static extern IntPtr Disassemble(
            [In] SafeCapstoneContextHandle pHandle,
            [In] byte[] pCode,
            [In] IntPtr codeSize,
            [In] ulong startingAddress,
            [In] IntPtr count,
            [Out] out IntPtr /* cs_insn * */ instruction);

        /// <summary>Free Memory Allocated For Disassembled Instructions.</summary>
        /// <param name="instructions">A pointer to a collection of disassembled
        /// instructions.</param>
        /// <param name="instructionCount">A platform specific integer representing
        /// the number of disassembled instructions.</param>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_free")]
        public static extern void Free(
            [In] IntPtr pInstructions,
            [In] IntPtr instructionCount);

        /// <summary>Report the last error number when some API function fail.
        /// Like glibc's errno, cs_errno might not retain its old value once accessed.</summary>
        /// <param name="pHandle"></param>
        /// <returns></returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_errno")]
        public static extern CapstoneErrorCode GetLastError(
            [In] SafeCapstoneContextHandle pHandle);

        /// <summary>Return friendly name of a group id (that an instruction can
        /// belong to) Find the group id from header file of corresponding
        /// architecture (arm.h for ARM, x86.h for X86, ...)
        /// WARN: when in 'diet' mode, this API is irrelevant because the engine does
        /// not store group name.</summary>
        /// <param name="pHandle">handle returned by cs_open()</param>
        /// <param name="groupId">group id</param>
        /// <returns>string name of the group, or NULL if @group_id is invalid.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_group_name", CharSet = CharSet.Ansi)]
        public static extern string GroupName(
            [In] SafeCapstoneContextHandle pHandle,
            [In] uint groupId);

        /// <summary>Resolve an Instruction Unique Identifier to an Instruction Name.
        /// </summary>
        /// <param name="pHandle">A pointer to a Capstone handle.</param>
        /// <param name="id">An instruction's unique identifier.</param>
        /// <returns>A pointer to a string representing the instruction's name. An
        /// <c>IntPtr.Zero</c> indicates the instruction's unique identifier is
        /// invalid.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_insn_name", CharSet=CharSet.Ansi)]
        public static extern string InstructionName(
            [In] SafeCapstoneContextHandle pHandle,
            [In] uint instructionId);

        /// <summary>Create a Capstone native handle that stands for a disassembler
        /// for the given architecture and mode pair.</summary>
        /// <param name="architecture">Target architecture.</param>
        /// <param name="mode">Target mode.</param>
        /// <param name="handle">On return this parameter is updated with the
        /// native handle value.</param>
        /// <returns>One of the predefined Capstone return codes.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_open")]
        internal static extern CapstoneErrorCode Open(
            [In] DisassembleArchitecture architecture,
            [In] DisassembleMode mode,
            [Out] out IntPtr handle);

        /// <summary>Resolve a Registry Unique Identifier to an Registry Name.</summary>
        /// <param name="pHandle">A pointer to a Capstone handle.</param>
        /// <param name="registryId">A registry's unique identifier.</param>
        /// <returns>A pointer to a string representing the registry's name. An
        /// <c>IntPtr.Zero</c> indicates the registry's unique identifier is invalid.
        /// </returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi, EntryPoint = "cs_reg_name")]
        public static extern string RegistryName(
            [In] SafeCapstoneContextHandle pHandle,
            [In] uint registryId);

        /// <summary>Set a Disassemble Option.</summary>
        /// <param name="pHandle">A Capston context (a.k.a a disassembler).</param>
        /// <param name="option">The option to be set.</param>
        /// <param name="value">The value to be set.</param>
        /// <returns>The return code.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_option")]
        internal static extern CapstoneErrorCode SetOption(
            [In] SafeCapstoneContextHandle pHandle,
            [In] DisassembleOptionType option,
            [In] IntPtr value);
    }
}