using Annex.Assets;
using SFML.Graphics;
using System.IO;

namespace Annex.Graphics.Sfml
{
    public class SfmlTextureInitializer : IAssetInitializer
    {
        public readonly string AssetPath;

        public SfmlTextureInitializer(string path) {
            this.AssetPath = path;
        }

        public object Load(IAssetInitializerArgs args, IAssetLoader assetLoader) {
            Debug.Assert(args is SfmlTextureInitializerArgs, $"{nameof(SfmlTextureInitializer)} requires {nameof(SfmlTextureInitializerArgs)} args");
            return new Texture(assetLoader.GetString(args.Key));
        }

        public bool Validate(IAssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".png");
        }
    }
}
