﻿using System;
using System.Runtime.InteropServices;

namespace CSCapstone.Sparc {
    /// <summary>
    ///     Native SPARC Instruction Detail.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeSparcInstructionDetail {
        /// <summary>
        ///     Instruction's Code Condition.
        /// </summary>
        public int CodeCondition;

        /// <summary>
        ///     Instruction's Branch Hint.
        /// </summary>
        public int BranchHint;

        /// <summary>
        ///     Number of Instruction's Operands.
        /// </summary>
        public byte OperandCount;

        /// <summary>
        ///     Instruction's First Operand.
        /// </summary>
        public NativeSparcInstructionOperand Operand1;

        /// <summary>
        ///     Instruction's Second Operand.
        /// </summary>
        public NativeSparcInstructionOperand Operand2;

        /// <summary>
        ///     Instruction's Third Operand.
        /// </summary>
        public NativeSparcInstructionOperand Operand3;

        /// <summary>
        ///     Instruction's Fourth Operand.
        /// </summary>
        public NativeSparcInstructionOperand Operand4;

        /// <summary>
        ///     Get Instruction's Managed Operands.
        /// </summary>
        /// <value>
        ///     Convenient property to retrieve the instruction's operands as a managed collection. The size of the
        ///     managed collection will always be equal to the value represented by
        ///     <c>NativeSparcInstructionDetail.OperandCount</c>. This property allocates managed memory for a new
        ///     managed collection and copies by value the required <c>NativeSparcInstructionDetail.Operand</c>
        ///     fields every time it is invoked.
        /// </value>
        public NativeSparcInstructionOperand[] ManagedOperands {
            get {
                // Create Managed Collection.
                //
                // Unfortunately, C# does not support fixed arrays, or buffers, for user defined structures. To work
                // around alignment issues with the default .NET Marshaller, and because the convenience of the
                // Capstone API allows it, each operand is declared as an individual field. The following ugly code
                // segment deals with composing those individual fields in an appropriately sized collection.
                //
                // I am so sorry for this ugly code.
                var managedOperands = new NativeSparcInstructionOperand[this.OperandCount];
                var thisType = this.GetType();
                for (var i = 1; i <= managedOperands.Length; i++) {
                    // Get Operand Using Reflection.
                    //
                    // This is a relatively expensive operation. An better approach needs to be investigated.
                    var operandPropertyName = String.Format("Operand{0}", i);
                    var operandProperty = thisType.GetField(operandPropertyName);
                    var operandPropertyValue = (NativeSparcInstructionOperand) operandProperty.GetValue(this);

                    managedOperands[(i - 1)] = operandPropertyValue;
                }

                return managedOperands;
            }
        }
    }
}