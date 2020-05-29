using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Managers;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using System.IO;
using static Annex.Paths;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static ICanvas Canvas => Locate<ICanvas>();

        public class DefaultCanvas : SfmlCanvas
        {
            public DefaultCanvas() 
                : base(new DefaultTextureManager(), new DefaultFontManager(), new DefaultIconManager()) {
            }
        }

        public class DefaultTextureManager : CachedAssetManager
        {
            public DefaultTextureManager() 
                : base(AssetType.Texture, new FileLoader(), new SfmlTextureInitializer(Path.Combine(ApplicationPath, "textures/"))) {
            }
        }

        public class DefaultFontManager : CachedAssetManager
        {
            public DefaultFontManager() 
                : base(AssetType.Font, new FileLoader(), new SfmlFontInitializer(Path.Combine(ApplicationPath, "fonts/"))) {
            }
        }

        public class DefaultIconManager : UncachedAssetManager
        {
            public DefaultIconManager()
                : base(AssetType.Icon, new FileLoader(), new SfmlIconInitializer(Path.Combine(ApplicationPath, "icons/"))) {

            }
        }
    }
}
