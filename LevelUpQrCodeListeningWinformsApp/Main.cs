using System;
using System.Windows.Forms;
using LevelUpQrCodeListenerLibrary;

namespace LevelUpQrCodeListeningWinformsApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LevelUpQrCodeListener.Instance.StartListener(LevelUpPaymentTokenFound);
        }

        private void LevelUpPaymentTokenFound(string levelUpPaymentToken)
        {
            richTextBoxMain.AppendText($"LevelUp QR code found: {levelUpPaymentToken}{Environment.NewLine}");
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            LevelUpQrCodeListener.Instance.StopListener();
        }
    }
}
