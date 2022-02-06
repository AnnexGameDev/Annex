using Annex.Core.Data;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics
{
    public interface IGraphicsService
    {
        IEnumerable<IWindow> Windows { get; }

        public IWindow GetWindow(string id);

        public IWindow CreateWindow(string id,
            Vector2ui? size = null, 
            Vector2i? position = null,
            string? title = null,
            WindowStyle style = WindowStyle.Default
        );
    }
}