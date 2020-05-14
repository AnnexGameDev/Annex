using Annex.Data.Shared;
using Annex.Graphics.Cameras;

namespace Annex.Graphics
{
    public interface ICanvas : IDrawableSurface, IHardwarePollable, IService
    {
        Vector GetResolution();
        Camera GetCamera();
        void SetVideoMode(VideoMode mode);
        void ChangeResolution(uint width, uint height);
        void SetVisible(bool visible);
        void ProcessEvents();
        void SetWindowIcon(string iconPath);

        void BeginDrawing();
        void EndDrawing();
    }
}
