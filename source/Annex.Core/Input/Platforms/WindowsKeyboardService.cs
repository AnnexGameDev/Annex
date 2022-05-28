#if WINDOWS
using System.Runtime.InteropServices;

namespace Annex_Old.Core.Input.Platforms
{
    internal class WindowsKeyboardService : IPlatformKeyboardService
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        private bool IsKeyToggled(VK key) {
            var state = GetKeyState((int)key);
            return (state & 0b1) > 0;
        }

        private bool IsKeyPressed(VK key) {
            var state = GetKeyState((int)key);
            return (state & 0b100000000000000) > 0;
        }
        
        public bool IsCapsLockOn() {
            return IsKeyToggled(VK.CAPITAL);
        }

        public bool IsNumLockOn() {
            return IsKeyToggled(VK.NUMLOCK);
        }

        public bool IsScrollLockOn() {
            return IsKeyToggled(VK.SCROLL);
        }

        public bool IsShiftPressed() {
            return IsKeyPressed(VK.LSHIFT) || IsKeyPressed(VK.RSHIFT);
        }

        public bool IsControlPressed() {
            return IsKeyPressed(VK.CONTROL);
        }
    }
}
#endif