using Annex.Graphics;
using Annex.Graphics.Sfml;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static ICanvas Canvas => Locate<ICanvas>() ?? Provide<ICanvas>(new SfmlCanvas(960, 640));
    }
}
