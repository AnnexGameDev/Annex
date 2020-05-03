using Annex.Graphics;
using Annex.Graphics.Sfml;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static Canvas Canvas => Locate<Canvas>() ?? Provide<Canvas, SfmlCanvas>();
    }
}
