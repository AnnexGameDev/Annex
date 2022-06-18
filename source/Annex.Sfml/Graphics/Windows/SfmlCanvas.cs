using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Graphics.PlatformTargets;
using Scaffold.Logging;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.Windows
{
    internal abstract class SfmlCanvas : ICanvas
    {
        protected readonly ICameraCache CameraCache;
        private readonly IPlatformTargetFactory _platformTargetFactory;
        protected abstract RenderTarget? _renderTarget { get; }

        public SfmlCanvas(IPlatformTargetFactory platformTargetFactory, ICameraCache cameraCache) {
            this._platformTargetFactory = platformTargetFactory;
            this.CameraCache = cameraCache;
        }

        public void Draw(DrawContext context) {
            var platformTarget = this._platformTargetFactory.GetPlatformTarget(context);

            if (platformTarget != null) {

                // Update the camera if we need to
                if (context.Camera != null) {
                    var view = this.CameraCache.GetCamera(context.Camera)?.View;

                    if (view == null) {
                        Log.Trace(LogSeverity.Error, $"Tried to set a view that doesn't exist: {context.Camera}");
                    } else {
                        this._renderTarget?.SetView(view);
                    }
                }

                platformTarget.TryDraw(this._renderTarget);
            }
        }
    }
}