using Annex.Graphics.Cameras;

namespace Annex.Graphics
{
    public interface ICanvas : IDrawableSurface, IHardwarePollable
    {
        Camera GetCamera();
        void SetVisible(bool visible);
    }
}
