using Annex_Old.Data.Shared;
using Annex_Old.Graphics.Cameras;
using Annex_Old.Graphics.Events;
using Annex_Old.Services;
using System;

namespace Annex_Old.Graphics
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
        void SetTitle(string title);
    }
}
