using Annex.Core.Data;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics
{
    internal class GraphicsService : IGraphicsService
    {
        private readonly IGraphicsEngine _graphicsEngine;

        private Dictionary<string, IWindow> _windows = new();
        public IEnumerable<IWindow> Windows => _windows.Values;

        public IWindow GetWindow(string id) {
            return this._windows[id];
        }

        public GraphicsService(IGraphicsEngine graphicsEngine) {
            this._graphicsEngine = graphicsEngine;
        }

        public IWindow CreateWindow(string id, 
            Vector2ui? size, 
            Vector2i? position,
            string? title,
            WindowStyle style
        ) {
            var window = this._graphicsEngine.CreateWindow(style);
            this._windows.Add(id, window);

            if (size != null) {
                window.Size = size;
            }

            if (position != null) {
                window.Position = position;
            }

            if (title != null) {
                window.Title = title;
            }

            return window;
        }
    }
}