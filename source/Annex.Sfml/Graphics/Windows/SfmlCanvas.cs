using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Graphics.PlatformTargets;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.Windows
{
    internal abstract class SfmlCanvas : ICanvas
    {
        private readonly IPlatformTargetFactory _platformTargetFactory;
        protected abstract RenderTarget? _renderTarget { get; }

        public SfmlCanvas(IPlatformTargetFactory platformTargetFactory) {
            this._platformTargetFactory = platformTargetFactory;
        }

        public void Draw(DrawContext context) {
            var platformTarget = this._platformTargetFactory.GetPlatformTarget(context);
            platformTarget?.TryDraw(this._renderTarget);
        }
    }
}