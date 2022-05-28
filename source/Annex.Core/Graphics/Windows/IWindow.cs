using Annex_Old.Core.Assets;
using Annex_Old.Core.Data;
using Annex_Old.Core.Input;

namespace Annex_Old.Core.Graphics.Windows
{
    public interface IWindow
    {
        IVector2<float> WindowResolution { get; }
        IVector2<float> WindowSize { get; }
        IVector2<float> WindowPosition { get; }
        WindowStyle WindowStyle { get; set; }
        string Title { get; set; }
        bool IsVisible { get; set; }

        void SetResolution(float x, float y);
        void SetSize(float x, float y);
        void SetPosition(float x, float y);

        Camera? GetCamera(CameraId cameraId);
        Camera? GetCamera(string cameraId);
        void AddCamera(Camera camera);

        void SetIcon(uint sizeX, uint sizeY, IAsset asset);
        void SetMouseImage(IAsset img, uint sizeX, uint sizeY, uint offsetX, uint offsetY);

        // Mouse
        IVector2<float> GetMousePos(CameraId cameraId = CameraId.UI);
        bool IsMouseButtonDown(MouseButton button);

        // Keyboard
        bool IsKeyDown(KeyboardKey key);

        // Controllers
        bool IsControllerConnected(uint controllerId);
        bool IsControllerButtonPressed(uint controllerId, ControllerButton button);
        float GetControllerJoystickAxis(uint controllerId, ControllerJoystickAxis axis);
    }
}