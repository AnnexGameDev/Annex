using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Graphics.PlatformTargets;
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
            this._container.Register<IPlatformTargetFactory, PlatformTargetFactory>();
            this._container.Register<ITextureCache, TextureCache>();
            this._container.Register<IFontCache, FontCache>();
            this._container.RegisterAggregate<IPlatformTargetCreator, TextPlatformTargetCreator>();
            this._container.RegisterAggregate<IPlatformTargetCreator, TexturePlatformTargetCreator>();
            this._container.RegisterAggregate<IPlatformTargetCreator, SpritesheetPlatformTargetCreator>();
            this._container.RegisterAggregate<IPlatformTargetCreator, SolidRectanglePlatformTargetCreator>();
            this._container.Register<ICameraCache, CameraCache>();
        }
    }
}