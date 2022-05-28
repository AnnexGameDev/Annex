using SFML.Graphics;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    public abstract class PlatformTarget : IDisposable
    {
        protected abstract void Draw(RenderTarget renderTarget);

        public void TryDraw(RenderTarget? renderTarget) {
            if (renderTarget != null) {
                this.Draw(renderTarget!);
            }
        }

        public abstract void Dispose();
    }
}
