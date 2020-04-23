using Annex.Data.Shared;
using Annex.Scenes;

namespace Annex.Graphics
{
    public interface IHardwarePollable
    {
        // Mouse
        Vector GetRealMousePos();
        Vector GetGameWorldMousePos();
        bool IsMouseButtonDown(MouseButton button);

        // Keyboard
        bool IsKeyDown(KeyboardKey key);

        // Joystick
        bool IsJoystickConnected(uint joystickId);
        bool IsJoystickButtonPressed(uint joystickId, JoystickButton button);
        float GetJoystickAxis(uint joystickId, JoystickAxis axis);
    }
}
