using Annex.Graphics.Contexts;

namespace Annex.Graphics
{
    public interface ICanvas
    {
        void Draw(TextContext ctx);
        void Draw(TextureContext ctx);
        void Draw(SpriteSheetContext sheet);
        void Draw(SolidRectangleContext rectangle);

        void BeginDrawing();
        void EndDrawing();

        void SetVisible(bool visible);
    }
}
