using System;
using System.Windows.Forms;
using LevelUpQrCodeListenerLibrary;

namespace LevelUpQrCodeListenerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Helpers.HideWindow();

            using (var listener = LevelUpQrCodeListener.Instance)
            {
                listener.StartListener(LevelUpPaymentTokenFound);

                Application.Run();

                listener.StopListener();
            }
        }
        
        private static void LevelUpPaymentTokenFound(string levelUpPaymentToken)
        {
            Console.WriteLine($"LevelUp QR code found: {levelUpPaymentToken}");
        }
    }
}
