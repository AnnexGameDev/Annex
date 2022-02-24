using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Graphics.Windows;
using Scaffold.DependencyInjection;

namespace Annex.Sfml.Graphics
{
    public class SfmlGraphicsEngine : IGraphicsEngine
    {
        private readonly IContainer _container;

        public IWindow CreateWindow() => this._container.Resolve<SfmlWindow>();

        public SfmlGraphicsEngine(IContainer container) {
            this._container = container;
            this._container.Register<SfmlWindow>();
        }
    }
}