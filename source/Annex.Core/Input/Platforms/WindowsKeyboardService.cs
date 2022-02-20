#if WINDOWS
using System.Runtime.InteropServices;

namespace Annex.Core.Input.Platforms
{
    internal class WindowsKeyboardService : IPlatformKeyboardService
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        private bool IsKeyDown(VK key) {
            var keyState = ((ushort)GetKeyState((int)key)) & 0xffffff;
            return keyState != 0;
        }
        
        public bool IsCapsLockOn() {
            return IsKeyDown(VK.CAPITAL);
        }

        public bool IsNumLockOn() {
            return IsKeyDown(VK.NUMLOCK);
        }

        public bool IsScrollLockOn() {
            return IsKeyDown(VK.SCROLL);
        }

        public bool IsShiftPressed() {
            return IsKeyDown(VK.LSHIFT) || IsKeyDown(VK.RSHIFT);
        }
    }
}
#endif