namespace Annex_Old.Core.Graphics
{
    public interface IDrawable : IDisposable
    {
        void Draw(ICanvas canvas);
    }
}