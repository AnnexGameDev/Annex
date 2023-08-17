using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics
{
    public interface IGraphicsService : IDisposable
    {
        IEnumerable<IWindow> Windows { get; }

        public IWindow GetWindow(string id);

        public IWindow CreateWindow(string id);
    }
}