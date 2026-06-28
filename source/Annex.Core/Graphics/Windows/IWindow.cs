using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input;
using Annex.Core.Input.InputEvents;
using Annex.Core.Scenes.Elements;

namespace Annex.Core.Graphics.Windows;

public interface IWindow : IDisposable
{
    Guid Id { get; }
    nint SystemHandle { get; }
    string Title { get; set; }
    bool IsVisible { get; set; }
    object Buffer { get; }
    uint BufferHeight { get; }
    uint BufferWidth { get; }

    uint ResolutionWidth { get; }
    uint ResolutionHeight { get; }

    uint Width { get; }
    uint Height { get; }

    int Left { get; }
    int Top { get; }

    void SetPosition(int x, int y);

    // Cameras
    (float top, float left, float bottom, float right) GetCameraBounds(string cameraId);
    (float x, float y) GetCameraPoint(string cameraId, MouseButtonPressedEvent @event);
    (float x, float y) GetCameraPoint(string cameraId, MouseButtonReleasedEvent @event);
    (float x, float y) GetCameraPoint(string cameraId, MouseMovedEvent @event);
    Camera? GetCamera(string cameraId);
    void AddCamera(Camera camera);

    void SetIcon(uint sizeX, uint sizeY, object asset);
    void SetMouseImage(object img, uint sizeX, uint sizeY, uint offsetX, uint offsetY);

    // Mouse
    Position GetMousePosition(string cameraId = KnownCamera.UI);
    bool IsMouseButtonDown(MouseButton button);

    // Keyboard
    bool IsKeyDown(KeyboardKey key);

    // Controllers
    bool IsControllerConnected(uint controllerId);
    bool IsControllerButtonPressed(uint controllerId, ControllerButton button);
    float GetControllerJoystickAxis(uint controllerId, ControllerJoystickAxis axis);

    // Scenes
    IScene Scene { get; }

    void LoadScene(Type sceneType, object? parameters = null, bool disposeOldScene = true);
    void LoadScene(IScene scene, object? parameters = null, bool disposeOldScene = true);
    void LoadScene<T>(object? parameters = null, bool disposeOldScene = true) where T : IScene;
    bool IsCurrentScene<T>() where T : IScene;

    // Graphics
    void DrawCurrentScene();
    void Draw(DrawContext context);
    void UpdateBuffer();
}