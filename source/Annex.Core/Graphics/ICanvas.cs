using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Graphics
{
    public interface ICanvas
    {
        void Draw(SpritesheetContext context);
        void Draw(TextureContext context);
    }
}