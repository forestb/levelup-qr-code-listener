namespace LevelUpQrCodeListenerApp
{
    public class Helpers
    {
        public static void HideWindow()
        {
            var handle = NativeMethods.GetConsoleWindow();
            NativeMethods.ShowWindow(handle, NativeMethods.SW_HIDE);
        }
    }
}
