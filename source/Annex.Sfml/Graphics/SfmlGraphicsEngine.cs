using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Graphics.Windows;

namespace Annex.Sfml.Graphics
{
    public class SfmlGraphicsEngine : IGraphicsEngine
    {
        private readonly ICoreEventService _coreEventService;

        public IWindow CreateWindow() => new SfmlWindow(this._coreEventService);

        public SfmlGraphicsEngine(ICoreEventService coreEventService) {
            this._coreEventService = coreEventService;
        }

    }
}