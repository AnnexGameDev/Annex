using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Graphics.Windows;

namespace Annex.Sfml.Graphics
{
    public class SfmlGraphicsEngine : IGraphicsEngine
    {
        public IWindow CreateWindow() => new SfmlWindow();
    }
}