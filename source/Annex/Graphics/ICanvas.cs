using Annex.Data.Shared;
using Annex.Graphics.Cameras;
using Annex.Services;

namespace Annex.Graphics
{
    public interface ICanvas : IDrawableSurface, IHardwarePollable, IService
    {
        bool IsActive { get; }

        Vector GetResolution();
        Camera GetCamera();
        void SetVideoMode(VideoMode mode);
        void ChangeResolution(uint width, uint height);
        void SetVisible(bool visible);
        void SetWindowIcon(string iconPath);
    }
}
