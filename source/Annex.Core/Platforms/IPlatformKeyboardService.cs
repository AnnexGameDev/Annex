namespace Annex.Core.Hardware
{
    public interface IPlatformKeyboardService
    {
        bool IsCapsLockOn();
        bool IsNumLockOn();
        bool IsScrollLockOn();
        bool IsShiftPressed();
        bool IsControlPressed();
    }
}