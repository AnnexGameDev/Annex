#nullable enable
using Annex.Data.Shared;
using Annex.Graphics.Cameras;

namespace Annex.Graphics
{
    public interface ICanvas : IDrawableSurface, IHardwarePollable, IService
    {
        public const string DrawGameEventID = "draw-game";

        Vector GetResolution();
        Camera GetCamera();
        void ChangeResolution(uint width, uint height);
        void SetVisible(bool visible);
        void ProcessEvents();
    }
}
