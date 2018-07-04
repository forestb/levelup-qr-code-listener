using System;
using System.Runtime.InteropServices;

namespace LevelUpQrCodeListenerApp
{
    internal static class NativeMethods
    {
        internal const int SW_HIDE = 0;

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
