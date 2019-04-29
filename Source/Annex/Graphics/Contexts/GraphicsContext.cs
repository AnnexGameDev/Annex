namespace Annex.Graphics.Contexts
{
    public abstract class GraphicsContext : IDrawableContext
    {
        public abstract void Draw(TextContext ctx);
        public abstract void Draw(SurfaceContext ctx);
    }
}
