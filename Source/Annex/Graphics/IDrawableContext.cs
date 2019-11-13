using Annex.Graphics.Contexts;

namespace Annex.Graphics
{
    public interface IDrawableContext
    {
        void Draw(TextContext ctx);
        void Draw(TextureContext ctx);
        void Draw(SpriteSheet sheet);
        void Draw(SolidRectangleContext rectangle);

        void BeginDrawing();
        void EndDrawing();

        void SetVisible(bool visible);
    }
}
