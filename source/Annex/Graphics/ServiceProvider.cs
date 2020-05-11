using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Resources.Loaders;
using Annex.Resources.Managers;
using System.IO;
using static Annex.Strings.Paths;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static Canvas Canvas => Locate<Canvas>();

        public class DefaultCanvas : SfmlCanvas
        {
            public DefaultCanvas() 
                : base(new DefaultTextureManager(), new DefaultFontManager()) {
            }
        }

        public class DefaultTextureManager : CachedResourceManager
        {
            public DefaultTextureManager() 
                : base(new FileLoader(), new SfmlTextureLoader(Path.Combine(ApplicationPath, "textures/"))) {
            }
        }

        public class DefaultFontManager : CachedResourceManager
        {
            public DefaultFontManager() 
                : base(new FileLoader(), new SfmlFontLoader(Path.Combine(ApplicationPath, "fonts/"))) {
            }
        }
    }
}
