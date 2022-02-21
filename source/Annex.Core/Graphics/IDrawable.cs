namespace Annex.Core.Graphics
{
    public interface IDrawable : IDisposable
    {
        void Draw(ICanvas canvas);
    }
}