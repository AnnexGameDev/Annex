using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input;
using Annex.Core.Scenes.Elements;

namespace Annex.Core.Graphics.Windows;

public interface IWindow : IDisposable
{
    Guid Id { get; }
    string Title { get; set; }
    bool IsVisible { get; set; }

    uint ResolutionWidth { get; }
    uint ResolutionHeight { get; }

    uint Width { get; }
    uint Height { get; }

    int Left { get; }
    int Top { get; }

    void SetPosition(int x, int y);

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

    // Scenes
    IScene Scene { get; }
    void LoadScene(IScene sceneInstance, object? parameters = null);
    void LoadScene<T>(object? parameters = null) where T : IScene;
    bool IsCurrentScene<T>() where T : IScene;

    // Graphics
    Task DrawCurrentSceneAsync();
    void Draw(DrawContext context);
}