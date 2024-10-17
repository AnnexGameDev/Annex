namespace Annex.Core.Graphics;

public interface IDrawable : IDisposable
{
    void DrawOn(ICanvas canvas);
}