using System;

namespace CSCapstone
{
    /// <summary>A collection of helper functions.</summary>
    internal static class Helpers
    {
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
