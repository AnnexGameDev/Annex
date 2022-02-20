namespace Annex.Core.Input.Platforms
{
    public interface IPlatformKeyboardService
    {
        bool IsCapsLockOn();
        bool IsNumLockOn();
        bool IsScrollLockOn();
        bool IsShiftPressed();
    }
}