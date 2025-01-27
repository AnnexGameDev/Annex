using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics;

public interface IGraphicsService : IDisposable
{
    IEnumerable<IWindow> Windows { get; }

    public IWindow GetWindow(Guid id);
    public IWindow CreateWindow(string title, uint width, uint height, WindowStyle style);
}