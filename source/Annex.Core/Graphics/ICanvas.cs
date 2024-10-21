using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Graphics;

public interface ICanvas
{
    void Draw(DrawContext context);
    void PostDraw();
    void PreDraw();
}