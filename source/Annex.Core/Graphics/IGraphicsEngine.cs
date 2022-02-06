using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics
{
    public interface IGraphicsEngine
    {
        IWindow CreateWindow(WindowStyle style);
    }
}