using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Input;

namespace Annex.Core.Graphics.Windows
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
        bool IsKeyDown(KeyboardKey key);
    }
}