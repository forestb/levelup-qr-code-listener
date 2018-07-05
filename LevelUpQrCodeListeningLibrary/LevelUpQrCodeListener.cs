using System;
using System.Diagnostics;
using System.Windows.Forms;
using LevelUpQrCodeListenerLibrary.Helpers;
using LevelUpQrCodeListenerLibrary.Wrappers;

namespace LevelUpQrCodeListenerLibrary
{
    public class LevelUpQrCodeListener : IDisposable
    {
        private static readonly Lazy<LevelUpQrCodeListener> Lazy =
            new Lazy<LevelUpQrCodeListener>(() => new LevelUpQrCodeListener());

        public static LevelUpQrCodeListener Instance => Lazy.Value;

        private LevelUpQrCodeListener()
        {

        }

        private IntPtr _hookId = IntPtr.Zero;

        private static Action<string> LevelUpPaymentTokenFound { get; set; }

        private readonly LevelUpPaymentTokenHelper _levelUpPaymentTokenHelper = new LevelUpPaymentTokenHelper();

        private readonly KeysConverter _keyConvert = new KeysConverter();

        private NativeMethodDelegates.LowLevelKeyboardProc _hookCallback;

        public void StartListener(Action<string> levelUpPaymentTokenFound)
        {
            LevelUpPaymentTokenFound = levelUpPaymentTokenFound;

            _hookCallback = HookCallback;

            GC.KeepAlive(_hookCallback);

            _hookId = SetHook(_hookCallback);
        }

        public void StopListener()
        {
            NativeMethods.UnhookWindowsHookEx(_hookId);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)NativeMethods.WM_KEYDOWN)
            {
                var key = KeyHelpers.ConvertToKey(lParam);

                ProcessKey(key);
            }

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private IntPtr SetHook(NativeMethodDelegates.LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(NativeMethods.WH_KEYBOARD_LL, proc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private void ProcessKey(Keys key)
        {
            bool isCandidateKeyPress = KeyHelpers.IsKeyAChar(key) || KeyHelpers.IsKeyADigit(key);

            bool shouldAppendKeyPress = isCandidateKeyPress;

            if (shouldAppendKeyPress)
            {
                string k = _keyConvert.ConvertToString(key);

                _levelUpPaymentTokenHelper.Buffer.Enqueue(k);

                if (_levelUpPaymentTokenHelper.ContainsDelimiters() && _levelUpPaymentTokenHelper.IsValid())
                {
                    string qrCode = _levelUpPaymentTokenHelper.GetPaymentToken();

                    LevelUpPaymentTokenFound?.Invoke(qrCode);

                    _levelUpPaymentTokenHelper.ClearBuffer();
                }
            }
        }

        public void Dispose()
        {
            StopListener();
        }
    }

    public class HookCallback
    {
    }
}
