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

        void SetResolution(float resolutionX, float resolutionY);
        void SetResolution(IVector2<float> newResolution);
        void SetSize(float sizeX, float sizeY);
        void SetSize(IVector2<float> newSize);
        void SetPosition(IVector2<float> newPosition);

        Camera? GetCamera(CameraId cameraId);
        Camera? GetCamera(string cameraId);
        void AddCamera(Camera camera);

        void SetIcon(uint sizeX, uint sizeY, IAsset asset);
        void SetMouseImage(IAsset img, uint sizeX, uint sizeY, uint offsetX, uint offsetY);
        bool IsKeyDown(KeyboardKey key);
    }
}