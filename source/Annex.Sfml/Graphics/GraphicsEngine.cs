using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Graphics.Windows;

namespace Annex.Sfml.Graphics
{
    public class GraphicsEngine : IGraphicsEngine
    {
        public IWindow CreateWindow(WindowStyle style) {
            return new SfmlWindow(style);
        }
    }
}