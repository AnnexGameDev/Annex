using Annex_Old.Core.Graphics.Windows;

namespace Annex_Old.Core.Graphics
{
    public interface IGraphicsService
    {
        IEnumerable<IWindow> Windows { get; }

        public IWindow GetWindow(string id);

        public IWindow CreateWindow(string id);
    }
}