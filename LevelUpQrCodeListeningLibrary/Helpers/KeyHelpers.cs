using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LevelUpQrCodeListenerLibrary.Helpers
{
    internal class KeyHelpers
    {
        public static bool IsKeyAChar(Keys key)
        {
            return key >= Keys.A && key <= Keys.Z;
        }

        public static bool IsKeyADigit(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        public static Keys ConvertToKey(IntPtr keyCode)
        {
            int vkCode = Marshal.ReadInt32(keyCode);

            return (Keys)vkCode;
        }
    }
}
