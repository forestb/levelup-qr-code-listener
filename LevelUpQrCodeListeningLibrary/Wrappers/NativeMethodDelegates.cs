using System;

namespace LevelUpQrCodeListenerLibrary.Wrappers
{
    internal static class NativeMethodDelegates
    {
        internal delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}
