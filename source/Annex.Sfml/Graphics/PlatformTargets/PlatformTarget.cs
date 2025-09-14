using Annex.Core.Graphics;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal abstract class PlatformTarget : IPlatformTarget
{
    public abstract object Target { get; }

    protected abstract void Draw(RenderTarget renderTarget);

    public void TryDraw(RenderTarget? renderTarget)
    {
        if (renderTarget != null)
        {
            Draw(renderTarget!);
        }
    }

    public abstract void Dispose();
}
