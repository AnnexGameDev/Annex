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
            Debug.Assert(graphicsEngine != null, "A singleton graphics engine must be registered");
            this._graphicsEngine = graphicsEngine;
        }

        public IWindow CreateWindow(string id) {
            var window = this._graphicsEngine.CreateWindow();
            this._windows.Add(id, window);
            return window;
        }
    }
}