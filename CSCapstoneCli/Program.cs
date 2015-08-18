﻿using CSCapstone;
using CSCapstone.Arm;
using CSCapstone.Arm64;
using CSCapstone.X86;
using System;

namespace CSCapstoneCli {
    /// <summary>
    ///     Main Program.
    /// </summary>
    internal static class Program {
        /// <summary>
        ///     Run Main Program.
        /// </summary>
        /// <param name="args">
        ///     A collection of arguments passed from the command line.
        /// </param>
        internal static void Main(string[] args) {
            Console.WriteLine("Capstone.NET Example Application");
            Console.WriteLine("Copyright (c) 2015 Ahmed Garhy (@9ee1)");
            Console.WriteLine("Powered by the Capstone Disassembly Framework");
            Console.WriteLine("Copyright (c) 2013 - 2014 Nguyen Anh Quynh");
            Console.WriteLine();

            Console.SetBufferSize(80, 480);
            Console.Write("Choose an Architecture (ARM32, ARM32-V8, ARM32-Thumb, ARM32-Thumb-MClass, ARM64, X86, X86-Iter): --> ");

            var architecture = Console.ReadLine();
            Console.WriteLine();
            switch (architecture) {
                case "ARM32":
                    Program.ShowArm(DisassemblerBase.SupportedMode.Arm32);
                    break;
                case "ARM32-V8":
                    Program.ShowArm(DisassemblerBase.SupportedMode.Arm32 | DisassemblerBase.SupportedMode.ArmV8);
                    break;
                case "ARM32-Thumb":
                    Program.ShowArm(DisassemblerBase.SupportedMode.ArmThumb);
                    break;
                case "ARM32-Thumb-MClass":
                    Program.ShowArm((int)DisassemblerBase.SupportedMode.ArmThumb + DisassemblerBase.SupportedMode.ArmCortexM);
                    break;
                case "ARM64":
                    Program.ShowArm64();
                    break;
                case "X86":
                    Program.ShowX86();
                    break;
                case "X86-Iter":
                    Program.ShowX86(true);
                    break;
                default:
                    Console.WriteLine("ERROR: You did not select a correct architecture. Good bye Steve!");
                    break;
            }

            Console.ReadLine();
        }

        internal static void ShowArm(DisassemblerBase.SupportedMode mode)
        {
            // Create ARM Disassembler.
            //
            // Creating the disassembler in a "using" statement ensures that resources get cleaned up automatically
            // when it is no longer needed.
            using (var disassembler = new ArmDisassembler(mode)) {
                // Enable Disassemble Details.
                //
                // Enables disassemble details, which are disabled by default, to provide more detailed information on
                // disassembled binary code.
                disassembler.EnableDetails = true;

                // Set Disassembler's Syntax.
                //
                // Make the disassembler generate instructions in Intel syntax.
                disassembler.Syntax = DisassemblerBase.SyntaxOptionValue.Intel;

                var code = new byte[0];
                switch (mode) {
                    case DisassemblerBase.SupportedMode.Arm32:
                        code = new byte[] {0xED, 0xFF, 0xFF, 0xEB, 0x04, 0xe0, 0x2d, 0xe5, 0x00, 0x00, 0x00, 0x00, 0xe0, 0x83, 0x22, 0xe5, 0xf1, 0x02, 0x03, 0x0e, 0x00, 0x00, 0xa0, 0xe3, 0x02, 0x30, 0xc1, 0xe7, 0x00, 0x00, 0x53, 0xe3, 0x00, 0x02, 0x01, 0xf1, 0x05, 0x40, 0xd0, 0xe8, 0xf4, 0x80, 0x00, 0x00};
                        break;
                    case DisassemblerBase.SupportedMode.Arm32 | DisassemblerBase.SupportedMode.ArmV8:
                        code = new byte[] { 0xe0, 0x3b, 0xb2, 0xee, 0x42, 0x00, 0x01, 0xe1, 0x51, 0xf0, 0x7f, 0xf5 };
                        break;
                    case DisassemblerBase.SupportedMode.ArmThumb:
                        code = new byte[] {0x70, 0x47, 0xeb, 0x46, 0x83, 0xb0, 0xc9, 0x68, 0x1f, 0xb1, 0x30, 0xbf, 0xaf, 0xf3, 0x20, 0x84};
                        break;
                    case DisassemblerBase.SupportedMode.ArmThumb | DisassemblerBase.SupportedMode.ArmCortexM:
                        code = new byte[] {0xef, 0xf3, 0x02, 0x80};
                        break;
                }

                // Disassemble All Binary Code.
                //
                // ...
                var instructions = disassembler.Disassemble(code);

                var hexCode = BitConverter.ToString(code).Replace("-", " ");
                Console.WriteLine(hexCode);
                Console.WriteLine();

                // Loop Through Each Disassembled Instruction.
                // ...
                foreach (var instruction in instructions) {
                    Console.WriteLine("{0:X}: \t {1} \t {2}", instruction.Address, instruction.Mnemonic, instruction.Operand);
                    Console.WriteLine("\t Id = {0}", instruction.InstructionId);

                    if (instruction.ArchitectureDetail != null) {
                        Console.WriteLine("\t CPS Flag = {0}", instruction.ArchitectureDetail.CpsFlag);
                        Console.WriteLine("\t CPS Mode = {0}", instruction.ArchitectureDetail.CpsMode);
                        Console.WriteLine("\t Code Condition = {0}", instruction.ArchitectureDetail.CodeCondition);
                        Console.WriteLine("\t Load User Mode Registers? {0}", instruction.ArchitectureDetail.LoadUserModeRegisters);
                        Console.WriteLine("\t Memory Barrier = {0}", instruction.ArchitectureDetail.MemoryBarrier);
                        Console.WriteLine("\t Operand Count: {0}", instruction.ArchitectureDetail.Operands.Length);

                        // Loop Through Each Instruction's Operands.
                        //
                        // ...
                        foreach (var operand in instruction.ArchitectureDetail.Operands) {
                            string operandValue = null;
                            switch (operand.Type) {
                                case ArmOperandType.CImmediate:
                                case ArmOperandType.Immediate:
                                case ArmOperandType.PImmediate:
                                    operandValue = ((ArmImmediateOperand)operand).Value.ToString("X");
                                    break;
                                case ArmOperandType.FloatingPoint:
                                    operandValue = ((ArmFloatingPointOperand)operand).Value.ToString();
                                    break;
                                case ArmOperandType.Memory:
                                    operandValue = "-->";
                                    break;
                                case ArmOperandType.Register:
                                    operandValue = ((ArmRegisterOperand)operand).Value.ToString();
                                    break;
                                case ArmOperandType.SetEnd:
                                    operandValue = ((ArmSetEndOperand)operand).Value.ToString();
                                    break;
                                case ArmOperandType.SysRegister:
                                    operandValue = ((ArmSystemRegisterOperand)operand).Value.ToString();
                                    break;
                            }

                            Console.WriteLine("\t\t {0} = {1}", operand.Type, operandValue);

                            // Handle Memory Operand.
                            //
                            // ...
                            if (operand.Type == ArmOperandType.Memory) {
                                ArmMemoryOperand memoryOperand = (ArmMemoryOperand)operand;
                                Console.WriteLine("\t\t\t Base Register = {0} ", memoryOperand.BaseRegister);
                                Console.WriteLine("\t\t\t Displacement = {0:X} ", memoryOperand.Displacement);
                                Console.WriteLine("\t\t\t Index Register = {0} ", memoryOperand.IndexRegister);
                                Console.WriteLine("\t\t\t Index Register Scale = {0} ", memoryOperand.IndexRegisterScale);
                                Console.WriteLine();
                            }

                            Console.WriteLine("\t\t\t Is Subtracted? = {0}", operand.IsSubtracted);
                            Console.WriteLine("\t\t\t Shifter = -->");
                            Console.WriteLine("\t\t\t\t Type = {0}", operand.Shifter.Type);
                            Console.WriteLine("\t\t\t\t Value = {0:X}", operand.Shifter.Value);

                            Console.WriteLine("\t\t\t Vector Index = {0}", operand.VectorIndex);
                        }

                        Console.WriteLine("\t Update Flags? {0}", instruction.ArchitectureDetail.UpdateFlags);
                        Console.WriteLine("\t Vector Data Type = {0}", instruction.ArchitectureDetail.VectorDataType);
                        Console.WriteLine("\t Vector Size= {0}", instruction.ArchitectureDetail.VectorSize);
                        Console.WriteLine("\t Write Back? {0}", instruction.ArchitectureDetail.WriteBack);
                    }

                    Console.WriteLine();
                }
            }
        }

        internal static void ShowArm64() {
            // Create ARM64 Disassembler.
            //
            // Creating the disassembler in a "using" statement ensures that resources get cleaned up automatically
            // when it is no longer needed.
            using (var disassembler = new Arm64Disassembler(DisassemblerBase.SupportedMode.Arm32)) {
                // Enable Disassemble Details.
                //
                // Enables disassemble details, which are disabled by default, to provide more detailed information on
                // disassembled binary code.
                disassembler.EnableDetails = true;

                // Set Disassembler's Syntax.
                //
                // Make the disassembler generate instructions in Intel syntax.
                disassembler.Syntax = DisassemblerBase.SyntaxOptionValue.Intel;

                // Disassemble All Binary Code.
                //
                // ...
                var code = new byte[] {0x09, 0x00, 0x38, 0xd5, 0xbf, 0x40, 0x00, 0xd5, 0x0c, 0x05, 0x13, 0xd5, 0x20, 0x50, 0x02, 0x0e, 0x20, 0xe4, 0x3d, 0x0f, 0x00, 0x18, 0xa0, 0x5f, 0xa2, 0x00, 0xae, 0x9e, 0x9f, 0x37, 0x03, 0xd5, 0xbf, 0x33, 0x03, 0xd5, 0xdf, 0x3f, 0x03, 0xd5, 0x21, 0x7c, 0x02, 0x9b, 0x21, 0x7c, 0x00, 0x53, 0x00, 0x40, 0x21, 0x4b, 0xe1, 0x0b, 0x40, 0xb9, 0x20, 0x04, 0x81, 0xda, 0x20, 0x08, 0x02, 0x8b, 0x10, 0x5b, 0xe8, 0x3c};
                var instructions = disassembler.DisassembleAll(code, 0x2C);

                var hexCode = BitConverter.ToString(code).Replace("-", " ");
                Console.WriteLine(hexCode);
                Console.WriteLine();

                // Loop Through Each Disassembled Instruction.
                // ...
                foreach (var instruction in instructions) {
                    Console.WriteLine("{0:X}: \t {1} \t {2}", instruction.Address, instruction.Mnemonic, instruction.Operand);
                    Console.WriteLine("\t Id = {0}", instruction.InstructionId);

                    if (instruction.ArchitectureDetail != null) {
                        Console.WriteLine("\t Code Condition = {0}", instruction.ArchitectureDetail.CodeCondition);
                        Console.WriteLine("\t Operand Count: {0}", instruction.ArchitectureDetail.Operands.Length);

                        // Loop Through Each Instruction's Operands.
                        //
                        // ...
                        foreach (var operand in instruction.ArchitectureDetail.Operands) {
                            string operandValue = null;
                            switch (operand.Type) {
                                case Arm64OperandType.CImmediate:
                                case Arm64OperandType.Immediate:
                                    operandValue = ((Arm64ImmediateOperand)operand).Value.ToString("X");
                                    break;
                                case Arm64OperandType.FloatingPoint:
                                    operandValue = ((Arm64FloatingPointOperand)operand).Value.ToString();
                                    break;
                                case Arm64OperandType.MemoryBarrierOperation:
                                    operandValue = ((Arm64MemoryBarrierOperand)operand).Value.ToString();
                                    break;
                                case Arm64OperandType.Memory:
                                    operandValue = "-->";
                                    break;
                                case Arm64OperandType.MrsRegister:
                                    operandValue = ((Arm64MrsRegisterOperand)operand).Value.ToString();
                                    break;
                                case Arm64OperandType.MsrRegister:
                                    operandValue = ((Arm64MsrRegisterOperand)operand).Value.ToString();
                                    break;
                                case Arm64OperandType.PState:
                                    operandValue = ((Arm64PStateOperand)operand).Value.ToString();
                                    break;
                                case Arm64OperandType.PrefetchOperation:
                                    operandValue = ((Arm64PrefetchOperand)operand).Value.ToString();
                                    break;
                                case Arm64OperandType.Register:
                                    operandValue = ((Arm64RegisterOperand)operand).Value.ToString();
                                    break;
                                case Arm64OperandType.SysOperation:
                                    operandValue = "-->";
                                    break;
                            }

                            Console.WriteLine("\t\t {0} = {1}", operand.Type, operandValue);

                            // Handle Memory Operand.
                            //
                            // ...
                            if (operand.Type == Arm64OperandType.Memory) {
                                Arm64MemoryOperand memoryOperand = (Arm64MemoryOperand)operand;
                                Console.WriteLine("\t\t\t Base Register = {0} ", memoryOperand.BaseRegister);
                                Console.WriteLine("\t\t\t Displacement = {0:X} ", memoryOperand.Displacement);
                                Console.WriteLine("\t\t\t Index Register = {0} ", memoryOperand.IndexRegister);
                                Console.WriteLine();
                            }

                            // Handle SYS Operation Operand.
                            //
                            // ...
                            if (operand.Type == Arm64OperandType.SysOperation) {
                                operandValue = null;
                                switch (instruction.InstructionId) {
                                    case Arm64Mnemonic.AT:
                                        operandValue = operand.AtInstructionOperation.ToString();
                                        break;
                                    case Arm64Mnemonic.DC:
                                        operandValue = operand.DcInstructionOperation.ToString();
                                        break;
                                    case Arm64Mnemonic.IC:
                                        operandValue = operand.IcInstructionOperation.ToString();
                                        break;
                                    case Arm64Mnemonic.TLBI:
                                        operandValue = operand.TlbiInstructionOperation.ToString();
                                        break;
                                }

                                Console.WriteLine("\t\t\t SYS Operation = {0}", operandValue);
                                Console.WriteLine();
                            }

                            Console.WriteLine("\t\t\t Extender = {0}", operand.Extender);
                            Console.WriteLine("\t\t\t Shifter = -->");
                            Console.WriteLine("\t\t\t\t Type = {0}", operand.Shifter.Type);
                            Console.WriteLine("\t\t\t\t Value = {0:X}", operand.Shifter.Value);

                            Console.WriteLine("\t\t\t Vector Arrangement Specifier = {0}", operand.VectorArrangementSpecifier);
                            Console.WriteLine("\t\t\t Vector Element Size Specifier = {0}", operand.VectorElementSizeSpecifier);
                            Console.WriteLine("\t\t\t Vector Index = {0}", operand.VectorIndex);
                        }

                        Console.WriteLine("\t Update Flags? {0}", instruction.ArchitectureDetail.UpdateFlags);
                        Console.WriteLine("\t Write Back? {0}", instruction.ArchitectureDetail.WriteBack);
                    }

                    Console.WriteLine();
                }
            }
        }

        private static bool OnWeakX86DisassembledInstruction(InstructionBase instruction,
            int size, ulong address)
        {
            return true;
        }

        private static bool OnX86DisassembledInstruction(Instruction<X86Mnemonic, X86Register, X86InstructionGroup, X86InstructionDetail> instruction,
            int size, ulong nextAddress)
        {
            Console.WriteLine("{0:X8} {1} {2}",
                instruction.Address, instruction.Mnemonic, instruction.Operand);
            return true;
        }

        internal static void ShowX86(bool iteratively = false) {
            // Create X86 Disassembler.
            //
            // Creating the disassembler in a "using" statement ensures that resources get cleaned up automatically
            // when it is no longer needed.
            using (var disassembler = new X86Disassembler(DisassemblerBase.SupportedMode.Bit32)) {
                // Enable Disassemble Details.
                //
                // Enables disassemble details, which are disabled by default, to provide more detailed information on
                // disassembled binary code.
                disassembler.EnableDetails = true;

                // Set Disassembler's Syntax.
                //
                // Make the disassembler generate instructions in Intel syntax.
                disassembler.Syntax = DisassemblerBase.SyntaxOptionValue.Intel;

                // Disassemble All Binary Code.
                //
                // ...
                var code = new byte[] {0x8d, 0x4c, 0x32, 0x08, 0x01, 0xd8, 0x81, 0xc6, 0x34, 0x12, 0x00, 0x00, 0x05, 0x23, 0x01, 0x00, 0x00, 0x36, 0x8b, 0x84, 0x91, 0x23, 0x01, 0x00, 0x00, 0x41, 0x8d, 0x84, 0x39, 0x89, 0x67, 0x00, 0x00, 0x8d, 0x87, 0x89, 0x67, 0x00, 0x00, 0xb4, 0xc6};

                if (iteratively) {
                    disassembler.Disassemble(code, OnX86DisassembledInstruction);
                    return;
                }
                var instructions = disassembler.Disassemble(code);

                var hexCode = BitConverter.ToString(code).Replace("-", " ");
                Console.WriteLine(hexCode);
                Console.WriteLine();

                // Loop Through Each Disassembled Instruction.
                // ...
                foreach (var instruction in instructions) {
                    Console.WriteLine("{0:X}: \t {1} \t {2}", instruction.Address, instruction.Mnemonic, instruction.Operand);
                    Console.WriteLine("\t Id = {0}", instruction.InstructionId);

                    if (instruction.ArchitectureDetail != null) {
                        Console.WriteLine("\t Address Size = {0}", instruction.ArchitectureDetail.AddressSize);
                        Console.WriteLine("\t AVX Code Condition = {0}", instruction.ArchitectureDetail.AvxCodeCondition);
                        Console.WriteLine("\t AVX Rounding Mode = {0}", instruction.ArchitectureDetail.AvxRoundingMode);
                        Console.WriteLine("\t Displacement = {0:X}", instruction.ArchitectureDetail.Displacement);
                        Console.WriteLine("\t Mod/Rm = {0:X}", instruction.ArchitectureDetail.ModRm);
                        Console.WriteLine("\t Operand Count: {0}", instruction.ArchitectureDetail.Operands.Length);

                        // Loop Through Each Instruction's Operands.
                        //
                        // ...
                        foreach (var operand in instruction.ArchitectureDetail.Operands) {
                            string operandValue = null;
                            switch (operand.Type) {
                                case X86OperandType.FloatingPoint:
                                    operandValue = ((X86FloatingPointOperand)operand).Value.ToString("X");
                                    break;
                                case X86OperandType.Immediate:
                                    operandValue = ((X86ImmediateOperand)operand).Value.ToString("X");
                                    break;
                                case X86OperandType.Memory:
                                    operandValue = "-->";
                                    break;
                                case X86OperandType.Register:
                                    operandValue = ((X86RegisterOperand)operand).Value.ToString();
                                    break;
                            }

                            Console.WriteLine("\t\t {0} = {1}", operand.Type, operandValue);

                            // Handle Memory Operand.
                            //
                            // ...
                            if (operand.Type == X86OperandType.Memory) {
                                X86MemoryOperand memoryOperand = (X86MemoryOperand)operand;
                                Console.WriteLine("\t\t\t Base Register = {0} ", memoryOperand.BaseRegister);
                                Console.WriteLine("\t\t\t Displacement = {0:X} ", memoryOperand.Displacement);
                                Console.WriteLine("\t\t\t Index Register = {0}", memoryOperand.IndexRegister);
                                Console.WriteLine("\t\t\t Index Register Scale = {0}", memoryOperand.IndexRegisterScale);
                                Console.WriteLine("\t\t\t Segment Register = {0}", memoryOperand.SegmentRegister);
                                Console.WriteLine();
                            }

                            Console.WriteLine("\t\t\t AVX Broadcast = {0}", operand.AvxBroadcast);
                            Console.WriteLine("\t\t\t AVX Zero Operation Mask? {0}", operand.AvxZeroOperationMask);
                        }

                        var hexOperationCode = BitConverter.ToString(instruction.ArchitectureDetail.OperationCode).Replace("-", " ");
                        Console.WriteLine("\t Operation Code = {0}", hexOperationCode);

                        var hexPrefix = String.Join(" ", instruction.ArchitectureDetail.Prefix);
                        Console.WriteLine("\t Prefix = {0}", hexPrefix);

                        Console.WriteLine("\t REX = {0}", instruction.ArchitectureDetail.Rex);
                        Console.WriteLine("\t SIB = {0}", instruction.ArchitectureDetail.Sib);
                        Console.WriteLine("\t SIB Base Register = {0}", instruction.ArchitectureDetail.SibBaseRegister);
                        Console.WriteLine("\t SIB Index Register = {0}", instruction.ArchitectureDetail.SibIndexRegister);
                        Console.WriteLine("\t SIB Scale = {0}", instruction.ArchitectureDetail.SibScale);
                        Console.WriteLine("\t SSE Code Condition = {0}", instruction.ArchitectureDetail.SseCodeCondition);
                        Console.WriteLine("\t Suppress All AVX Exceptions? {0}", instruction.ArchitectureDetail.SuppressAllAvxExceptions);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}