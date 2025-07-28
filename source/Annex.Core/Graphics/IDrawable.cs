using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics;

public interface IDrawable : IDisposable
{
    void DrawOn(IWindow window, long timeDelta);
}