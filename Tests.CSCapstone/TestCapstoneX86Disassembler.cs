using CSCapstone;
using CSCapstone.X86;
using NUnit.Framework;

namespace Tests.CSCapstone {
    /// <summary>
    ///     Test Capstone X86 Disassembler.
    /// </summary>
    [TestFixture]
    public sealed class TestCapstoneX86Disassembler {
        /// <summary>Test Create.</summary>
        [Test]
        public void TestCreate() {
            try {
                using (var disassembler = new X86Disassembler(DisassemblerBase.SupportedMode.Bit32)) {
                    Assert.IsNotNull(disassembler);
                    Assert.AreEqual(disassembler.Architecture, DisassemblerBase.SupportedArchitecture.X86);
                    Assert.AreEqual(disassembler.EnableDetails, false);
                    Assert.AreEqual(disassembler.Mode, DisassemblerBase.SupportedMode.Bit32);
                    Assert.AreEqual(disassembler.Syntax, DisassemblerBase.SyntaxOptionValue.Default);
                }
            }
            catch (System.Exception e) {
                for (System.Exception ex = e; null != ex; ex = ex.InnerException) {
                    System.Console.Write(ex.StackTrace);
                }
                throw;
            }
        }

        /// <summary>
        ///     Test Disassemble.
        /// </summary>
        [Test]
        public void TestDisassemble() {
            // Create X86 Disassembler.
            //
            // Creating the disassembler in a "using" statement ensures that resources get cleaned up automatically
            // when it is no longer needed.
            using (var disassembler = new X86Disassembler(DisassemblerBase.SupportedMode.Bit32)) {
                Assert.IsNotNull(disassembler);

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
                var code = new byte[] { 0x8d, 0x4c, 0x32, 0x08, 0x01, 0xd8, 0x81, 0xc6, 0x34, 0x12, 0x00, 0x00, 0x05, 0x23, 0x01, 0x00, 0x00, 0x36, 0x8b, 0x84, 0x91, 0x23, 0x01, 0x00, 0x00, 0x41, 0x8d, 0x84, 0x39, 0x89, 0x67, 0x00, 0x00, 0x8d, 0x87, 0x89, 0x67, 0x00, 0x00, 0xb4, 0xc6 };
                var instructions = disassembler.Disassemble(code);
                Assert.AreEqual(instructions.Length, 9);
            }
        }
    }
}