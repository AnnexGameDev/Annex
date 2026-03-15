using Annex.Core.Graphics.Windows;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Core.Graphics;

public interface IGraphicsService : IDisposable
{
    IEnumerable<IWindow> Windows { get; }

    bool TryGetWindow(Guid id, [NotNullWhen(true)] out IWindow? window);
    void DestroyWindow(Guid id);
    IWindow CreateWindow(string title, uint width, uint height, WindowStyle style);
}