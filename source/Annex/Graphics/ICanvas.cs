using Annex.Data.Shared;
using Annex.Graphics.Cameras;
using Annex.Graphics.Events;
using Annex.Services;
using System;

namespace Annex.Graphics
{
    public interface ICanvas : IDrawableSurface, IHardwarePollable, IService
    {
        bool IsActive { get; }

        event EventHandler<WindowResizedEvent>? OnWindowResized;

        Vector GetResolution();
        Camera GetCamera();
        void SetVideoMode(VideoMode mode);
        void ChangeResolution(uint width, uint height);
        void SetVisible(bool visible);
        void SetWindowIcon(string iconPath);
    }
}
