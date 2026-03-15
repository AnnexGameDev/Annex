using Annex.Core.Graphics.Windows;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Core.Graphics;

internal class GraphicsService : IGraphicsService
{
    private readonly IGraphicsEngine _graphicsEngine;

    private Dictionary<Guid, IWindow> _windows = new();
    public IEnumerable<IWindow> Windows => _windows.Values;

    public bool TryGetWindow(Guid id, [NotNullWhen(true)] out IWindow? window) => _windows.TryGetValue(id, out window);

    public GraphicsService(IGraphicsEngine graphicsEngine)
    {
        Debug.Assert(graphicsEngine != null, "A singleton graphics engine must be registered");
        _graphicsEngine = graphicsEngine;
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

    public void DestroyWindow(Guid id)
    {
        _windows[id].Dispose();
        _windows.Remove(id);
    }
}