using Annex.Graphics.Contexts;

namespace Annex.Graphics
{
    public interface IDrawableSurface
    {
       void Draw(TextContext ctx);
       void Draw(TextureContext ctx);
       void Draw(BatchTextureContext batch);
       void Draw(SpriteSheetContext sheet);
       void Draw(SolidRectangleContext rectangle);
    }
}
