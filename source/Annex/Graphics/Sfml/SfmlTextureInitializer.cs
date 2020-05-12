using Annex.Assets;
using SFML.Graphics;
using System.IO;
using static Annex.Graphics.Sfml.Errors;

namespace Annex.Graphics.Sfml
{
    public class SfmlTextureInitializer : IAssetInitializer
    {
        public string AssetPath { get; set; }

        public SfmlTextureInitializer(string path) {
            this.AssetPath = path;
        }

        public object Load(IAssetInitializerArgs args, IAssetLoader assetLoader) {
            Debug.Assert(args is SfmlTextureInitializerArgs, INVALID_INITIALIZER_ARGS.Format(nameof(SfmlTextureInitializer), nameof(SfmlTextureInitializerArgs)));
            return new Texture(assetLoader.GetString(args.Key));
        }

        public bool Validate(IAssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".png");
        }
    }
}
