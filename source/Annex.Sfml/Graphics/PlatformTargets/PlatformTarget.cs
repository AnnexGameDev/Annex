using SFML.Graphics;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    public abstract class PlatformTarget : IDisposable
    {
        public abstract void Dispose();

        protected abstract void Draw(RenderTarget renderTarget);

        public void TryDraw(RenderTarget? renderTarget) {
            if (renderTarget != null) {
                this.Draw(renderTarget!);
            }
        }
    }
}
