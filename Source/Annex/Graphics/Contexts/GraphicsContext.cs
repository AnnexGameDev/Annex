namespace Annex.Graphics.Contexts
{
    public abstract class GraphicsContext : IDrawableContext
    {
        public abstract void Draw(TextContext ctx);
        public abstract void Draw(SurfaceContext ctx);
        public abstract void BeginDrawing();
        public abstract void EndDrawing();
        public abstract void SetVisible(bool visible);
    }
}
