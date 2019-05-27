using Annex.UserInterface;

namespace Annex.Graphics.Contexts
{
    public abstract class GraphicsContext : IDrawableContext
    {
        public abstract void Draw(TextContext ctx);
        public abstract void Draw(SurfaceContext ctx);
        public abstract void BeginDrawing();

        // User Interface
        public abstract bool IsMouseButtonDown(MouseButton button);
        public abstract bool IsKeyDown(KeyboardKey key);

        public abstract void EndDrawing();
        public abstract void SetVisible(bool visible);
    }
}
