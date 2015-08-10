using System;
using System.Runtime.InteropServices;

namespace CSCapstone {
    /// <summary>
    ///     Capstone Proxy Import.
    /// </summary>
    public static class CapstoneProxyImport {
        [DllImport("CSCapstone.Proxy.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CapstoneArmDetail")]
        public static extern IntPtr ArmDetail(IntPtr pDetail);

        [DllImport("CSCapstone.Proxy.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CapstoneArm64Detail")]
        public static extern IntPtr Arm64Detail(IntPtr pDetail);

        [DllImport("CSCapstone.Proxy.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CapstoneX86Detail")]
        public static extern IntPtr X86Detail(IntPtr pDetail);
    }
}