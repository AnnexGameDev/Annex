using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics;

internal class GraphicsService : IGraphicsService
{
    private readonly IGraphicsEngine _graphicsEngine;

    private Dictionary<Guid, IWindow> _windows = new();
    public IEnumerable<IWindow> Windows => _windows.Values;

    public IWindow GetWindow(Guid id)
    {
        return _windows[id];
    }

    public GraphicsService(IGraphicsEngine graphicsEngine)
    {
        Debug.Assert(graphicsEngine != null, "A singleton graphics engine must be registered");
        this._graphicsEngine = graphicsEngine;
    }

    public IWindow CreateWindow(string title, uint width, uint height, WindowStyle style)
    {
        var window = _graphicsEngine.CreateWindow(title, width, height, style);
        _windows.Add(window.Id, window);
        return window;
    }

    public void Dispose()
    {
        foreach (var window in Windows)
        {
            window.Dispose();
        }
    }
}