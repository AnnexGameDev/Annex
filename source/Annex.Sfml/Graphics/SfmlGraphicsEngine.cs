using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input;
using Annex.Sfml.Graphics.Windows;

namespace Annex.Sfml.Graphics
{
    public class SfmlGraphicsEngine : IGraphicsEngine
    {
        private readonly ICoreEventService _coreEventService;
        private readonly IInputHandlerService _inputHandlerService;

        public IWindow CreateWindow() => new SfmlWindow(this._coreEventService, this._inputHandlerService);

        public SfmlGraphicsEngine(ICoreEventService coreEventService, IInputHandlerService inputHandlerService) {
            this._coreEventService = coreEventService;
            this._inputHandlerService = inputHandlerService;
        }

    }
}