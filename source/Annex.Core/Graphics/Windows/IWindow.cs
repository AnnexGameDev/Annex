using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Input;

namespace Annex.Core.Graphics.Windows
{
    public interface IWindow
    {
        Vector2ui WindowResolution { get; }
        Vector2ui WindowSize { get; }
        Vector2i WindowPosition { get; }
        WindowStyle WindowStyle { get; set; }
        string Title { get; set; }
        bool IsVisible { get; set; }

        Camera? GetCamera(string cameraId);
        void AddCamera(Camera camera);

        void SetIcon(uint sizeX, uint sizeY, IAsset asset);
        void SetMouseImage(IAsset img, uint sizeX, uint sizeY, uint offsetX, uint offsetY);
        bool IsKeyDown(KeyboardKey key);
    }
}