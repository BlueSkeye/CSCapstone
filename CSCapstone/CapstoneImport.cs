using System;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>Capstone Import.</summary>
    internal static class CapstoneImport
    {
        /// <summary>Allocate memory for 1 instruction to be used by cs_disasm_iter().
        /// </summary>
        /// <param name="pHandle">handle returned by <see cref="Open"/></param>
        /// <returns>when no longer in use, you can reclaim the memory allocated for
        /// this instruction with cs_free(insn, 1)</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_malloc")]
        internal static extern SafeNativeInstructionHandle AllocateInstruction(
            [In] SafeCapstoneContextHandle pHandle);

        /// <summary>Check if a disassembled instruction belong to a particular group.
        /// Find the group id from header file of corresponding architecture (arm.h
        /// for ARM, x86.h for X86, ...)
        /// Internally, this simply verifies if @group_id matches any member of
        /// insn->groups array.
        /// NOTE: this API is only valid when detail option is ON (which is OFF by
        /// default).
        /// WARN: when in 'diet' mode, this API is irrelevant because the engine
        /// does not update @groups array.</summary>
        /// <param name="pHandle">handle returned by <see cref="Open"/></param>
        /// <param name="insn">disassembled instruction structure received from
        /// cs_disasm() or cs_disasm_iter()</param>
        /// <param name="group_id">group that you want to check if this instruction
        /// belong to.</param>
        /// <returns>true if this instruction indeed belongs to aboved group, or
        /// false otherwise.</returns>
        /// <remarks>TODO : Define a method in NativeInstruction to leverage this
        /// function.</remarks>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_insn_group")]
        internal static extern bool BelongsToGroup(
            [In] SafeCapstoneContextHandle pHandle,
            [In] IntPtr /* cs_insn * */ insn,
            [In] uint group_id);

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

        /// <summary>Count the number of operands of a given type. Find the operand
        /// type in header file of corresponding architecture (arm.h for ARM, x86.h for X86, ...)
        /// </summary>
        /// <param name="pHandle">handle returned by cs_open()</param>
        /// <param name="insn">disassembled instruction structure received from
        /// cs_disasm() or cs_disasm_iter()</param>
        /// <param name="op_type">Operand type to be found.</param>
        /// <returns>number of operands of given type @op_type in instruction @insn,
        /// or -1 on failure.</returns>
        /// <remarks>TODO : Add a use case.</remarks>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_op_count")]
        internal static extern int CountOperands(
            [In] SafeCapstoneContextHandle pHandle,
            [In] IntPtr /* cs_insn * */ insn,
            [In] uint op_type);

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
        internal static extern IntPtr Disassemble(
            [In] SafeCapstoneContextHandle pHandle,
            [In] byte[] pCode,
            [In] IntPtr codeSize,
            [In] ulong startingAddress,
            [In] IntPtr count,
            [Out] out IntPtr /* cs_insn * */ instruction);

        /// <summary>Fast API to disassemble binary code, given the code buffer, codeSize,
        /// address and number of instructions to be decoded.
        /// This API put the resulted instruction into a given cache in @insn.</summary>
        /// <param name="pHandle">A <see cref="SafeCapstoneContextHandle"/> as retrieved
        /// by a call to <see cref="Open"/>.</param>
        /// <param name="code">buffer containing raw binary code to be disassembled</param>
        /// <param name="codeSize">codeSize of above code</param>
        /// <param name="address">address of the first insn in given raw code buffer</param>
        /// <param name="insn">pointer to instruction to be filled in by this API.</param>
        /// <returns>true if this API successfully decode 1 instruction, or false
        /// otherwise.</returns>
        /// <remarks>
        /// TODO : This function is not used for now. Define a clean public method
        /// or set of methods in the Disassembler class.
        /// 
        /// NOTE 1: this API will update @code, @codeSize & @address to point to
        /// the next instruction in the input buffer. Therefore, it is convenient to
        /// use cs_disasm_iter() inside a loop to quickly iterate all the instructions.
        /// While decoding one instruction at a time can also be achieved with
        /// cs_disasm(count=1), some benchmarks shown that cs_disasm_iter() can be
        /// 30% faster on random input.
        /// NOTE 2: the cache in @insn can be created with cs_malloc() API.
        /// NOTE 3: for system with scarce memory to be dynamically allocated such as
        /// OS kernel or firmware, this API is recommended over cs_disasm(), which
        /// allocates memory based on the number of instructions to be disassembled.
        /// The reason is that with cs_disasm(), based on limited available memory,
        /// we have to calculate in advance how many instructions to be disassembled,
        /// which complicates things. This is especially troublesome for the case
        /// @count=0, when cs_disasm() runs uncontrollably (until either end of input
        /// buffer, or when it encounters an invalid instruction).</remarks>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_disasm_iter")]
        internal static extern bool DisassembleIteratively(
            [In] SafeCapstoneContextHandle pHandle,
            [In, Out] ref IntPtr /* uint8_t ** */ code,
            [In, Out] ref IntPtr codeSize,
            [In, Out] ref ulong address,
            [In] SafeNativeInstructionHandle /* cs_insn * */ insn);

        /// <summary>Free Memory Allocated For Disassembled Instructions.</summary>
        /// <param name="instructions">A pointer to a collection of disassembled
        /// instructions.</param>
        /// <param name="instructionCount">A platform specific integer representing
        /// the number of disassembled instructions.</param>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_free")]
        [System.Security.SuppressUnmanagedCodeSecurity()]
        internal static extern void Free(
            [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SafeNativeInstructionHandle.Marshaler), MarshalCookie = SafeNativeInstructionHandle.Marshaler.FreeMarshalerCookie)] SafeNativeInstructionHandle instructions,
            [In] IntPtr instructionCount);

        /// <summary>Return a string describing given error code.</summary>
        /// <param name="errorCode"></param>
        /// <returns>returns a pointer to a string that describes the error code
        /// passed in the argument</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_strerror", CharSet = CharSet.Ansi)]
        internal static extern string GetErrorText(
            [In] CapstoneErrorCode errorCode);

        /// <summary>Report the last error number when some API function fail.
        /// Like glibc's errno, cs_errno might not retain its old value once accessed.</summary>
        /// <param name="pHandle"></param>
        /// <returns></returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_errno")]
        internal static extern CapstoneErrorCode GetLastError(
            [In] SafeCapstoneContextHandle pHandle);

        /// <summary></summary>
        /// <param name="major">On return will contain the major library version.</param>
        /// <param name="minor">On return will contain the minor library version.</param>
        /// <returns>combined API version & major and minor version numbers.
        /// hexical number as (major << 8 | minor), which encodes both major & minor
        /// versions.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_version")]
        internal static extern uint GetLibraryVersion(
            [Out] out int major,
            [Out] out int minor);

        /// <summary>Count the number of operands of a given type. Find the operand
        /// type in header file of corresponding architecture (arm.h for ARM, x86.h for X86, ...)
        /// </summary>
        /// <param name="pHandle">handle returned by cs_open()</param>
        /// <param name="insn">disassembled instruction structure received from
        /// cs_disasm() or cs_disasm_iter()</param>
        /// <param name="op_type">Operand type to be found.</param>
        /// <param name="position">position of the operand to be found. This must
        /// be in the range [1, cs_op_count(handle, insn, op_type)]</param>
        /// <returns>number of operands of given type @op_type in instruction @insn,
        /// or -1 on failure.</returns>
        /// <remarks>TODO : Add a use case.</remarks>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_op_index")]
        internal static extern int GetOperandIndex(
            [In] SafeCapstoneContextHandle pHandle,
            [In] IntPtr /* cs_insn * */ insn,
            [In] uint op_type,
            [In] uint position);

        /// <summary>Return friendly name of a group id (that an instruction can
        /// belong to) Find the group id from header file of corresponding
        /// architecture (arm.h for ARM, x86.h for X86, ...)
        /// WARN: when in 'diet' mode, this API is irrelevant because the engine does
        /// not store group name.</summary>
        /// <param name="pHandle">handle returned by cs_open()</param>
        /// <param name="groupId">group id</param>
        /// <returns>string name of the group, or NULL if @group_id is invalid.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_group_name", CharSet = CharSet.Ansi)]
        internal static extern string GroupName(
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
        internal static extern string InstructionName(
            [In] SafeCapstoneContextHandle pHandle,
            [In] uint instructionId);

        /// <summary> Check if a disassembled instruction IMPLICITLY used a particular
        /// register. Find the register id from header file of corresponding architecture
        /// (arm.h for ARM, x86.h for X86, ...) Internally, this simply verifies if
        /// @reg_id matches any member of insn->regs_read array.</summary>
        /// <param name="pHandle">handle returned by cs_open()</param>
        /// <param name="insn">disassembled instruction structure received from
        /// cs_disasm() or cs_disasm_iter()</param>
        /// <param name="reg_id">register that you want to check if this instruction
        /// used it.</param>
        /// <returns>true if this instruction indeed implicitly used aboved register,
        /// or false otherwise.</returns>
        /// <remarks>
        ///  NOTE: this API is only valid when detail option is ON (which is OFF by default)
        ///  when in 'diet' mode, this API is irrelevant because the engine does not
        ///  update @regs_read array.
        ///  TODO : not used. Add a use case.</remarks>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_reg_read")]
        internal static extern bool IsRegisterRead(
            [In] SafeCapstoneContextHandle pHandle,
            [In] IntPtr /* cs_insn * */ insn,
            [In] uint reg_id);

        /// <summary> Check if a disassembled instruction IMPLICITLY modified a particular
        /// register. Find the register id from header file of corresponding architecture
        /// (arm.h for ARM, x86.h for X86, ...) Internally, this simply verifies if
        /// @reg_id matches any member of insn->regs_read array.</summary>
        /// <param name="pHandle"></param>
        /// <param name="insn">disassembled instruction structure received from
        /// cs_disasm() or cs_disasm_iter()</param>
        /// <param name="reg_id">register that you want to check if this instruction
        /// used it.</param>
        /// <returns>true if this instruction indeed implicitly used aboved register,
        /// or false otherwise.</returns>
        /// <remarks>
        ///  NOTE: this API is only valid when detail option is ON (which is OFF by default)
        ///  when in 'diet' mode, this API is irrelevant because the engine does not
        ///  update @regs_read array.
        ///  TODO : not used. Add a use case.</remarks>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_reg_write")]
        internal static extern bool IsRegisterWrite(
            [In] SafeCapstoneContextHandle pHandle,
            [In] IntPtr /* cs_insn * */ insn,
            [In] uint reg_id);

        /// <summary> This API can be used to either ask for archs supported by this
        /// library, or check to see if the library was compile with 'diet' option
        /// (or called in 'diet' mode).</summary>
        /// <param name="query">To check if a particular arch is supported by this library, set @query
        /// to arch mode (CS_ARCH_* value). To verify if this library supports all
        /// the archs, use CS_ARCH_ALL. To check if this library is in 'diet' mode,
        /// set @query to CS_SUPPORT_DIET.</param>
        /// <returns> True if this library supports the given arch, or in 'diet'
        /// mode.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_support")]
        internal static extern bool IsSupported(SpecialMode query);

        /// <summary>Create a Capstone native handle that stands for a disassembler
        /// for the given architecture and mode pair.</summary>
        /// <param name="architecture">Target architecture.</param>
        /// <param name="mode">Target mode.</param>
        /// <param name="handle">On return this parameter is updated with the
        /// native handle value.</param>
        /// <returns>One of the predefined Capstone return codes.</returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_open")]
        internal static extern CapstoneErrorCode Open(
            [In] DisassemblerBase.SupportedArchitecture architecture,
            [In] DisassemblerBase.SupportedMode mode,
            [Out] out IntPtr handle);

        /// <summary>Resolve a Registry Unique Identifier to an Registry Name.</summary>
        /// <param name="pHandle">A pointer to a Capstone handle.</param>
        /// <param name="registryId">A registry's unique identifier.</param>
        /// <returns>A pointer to a string representing the registry's name. An
        /// <c>IntPtr.Zero</c> indicates the registry's unique identifier is invalid.
        /// </returns>
        [DllImport("capstone.dll", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi, EntryPoint = "cs_reg_name")]
        internal static extern string RegistryName(
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
            [In] DisassemblerBase.Option option,
            [In] IntPtr value);

        /// <summary>For use by  the IsSupported function.</summary>
        internal enum SpecialMode
        {
            AllArchitecture = 0xFFFF,
            DietMode = 0x10000,
            X86Reduce = 0x10001,
        }
    }
}