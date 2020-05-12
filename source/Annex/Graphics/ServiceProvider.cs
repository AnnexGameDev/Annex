using Annex.Assets.Loaders;
using Annex.Assets.Managers;
using Annex.Graphics;
using Annex.Graphics.Sfml;
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

        public class DefaultTextureManager : CachedAssetManager
        {
            public DefaultTextureManager() 
                : base(new FileLoader(), new SfmlTextureInitializer(Path.Combine(ApplicationPath, "textures/"))) {
            }
        }

        public class DefaultFontManager : CachedAssetManager
        {
            public DefaultFontManager() 
                : base(new FileLoader(), new SfmlFontLoader(Path.Combine(ApplicationPath, "fonts/"))) {
            }
        }
    }
}
