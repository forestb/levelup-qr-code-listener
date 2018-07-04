using System;
using LevelUpQrCodeListenerLibrary.DataStructures;

namespace LevelUpQrCodeListenerLibrary.Helpers
{
    internal class LevelUpPaymentTokenHelper
    {
        internal readonly FixedSizeQueue<string> Buffer = new FixedSizeQueue<string>(MAX_SIZE);

        private const string DELIMITER = "LU";
        private const int MIN_SIZE = 32;
        private const int MAX_SIZE = 64;

        public bool ContainsDelimiters()
        {
            if (string.IsNullOrEmpty(Buffer.ConvertToString()))
            {
                return false;
            }

            if (DelimiterCount < 2)
            {
                return false;
            }

            return true;
        }

        public bool IsValid()
        {
            string paymentToken = GetPaymentToken();

            return paymentToken.StartsWith(DELIMITER, StringComparison.OrdinalIgnoreCase) &&
                   paymentToken.EndsWith(DELIMITER, StringComparison.OrdinalIgnoreCase) &&
                   paymentToken.Length >= MIN_SIZE &&
                   paymentToken.Length <= MAX_SIZE &&
                   paymentToken.IndexOfAny(new[] { ' ', '\r', '\n' }) == -1;
        }

        public string GetPaymentToken()
        {
            string[] splitResults = Buffer.ConvertToString().Split(new string[] {DELIMITER}, StringSplitOptions.None);

            int startIndex = splitResults.Length - 2;

            return string.Concat(DELIMITER, splitResults[startIndex], DELIMITER);
        }

        public void ClearBuffer()
        {
            string result;

            foreach (string s in Buffer)
            {
                if (s != null) Buffer.TryDequeue(out result);
            }
        }

        private int DelimiterCount =>
            Buffer.ConvertToString().Split(new string[] { DELIMITER }, StringSplitOptions.None).Length - 1;
    }
}
