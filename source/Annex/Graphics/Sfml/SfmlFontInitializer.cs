using Annex.Assets;
using SFML.Graphics;
using System.IO;
using static Annex.Graphics.Sfml.Errors;

namespace Annex.Graphics.Sfml
{
    public class SfmlFontInitializer : IAssetInitializer
    {
        public string AssetPath { get; set; }

        public SfmlFontInitializer(string path) {
            this.AssetPath = path;
        }

        public void CopyAssetToBinary(string path, IAssetLoader assetLoader) {
            throw new System.NotImplementedException();
        }

        public object Load(IAssetInitializerArgs args, IAssetLoader assetLoader) {
            Debug.Assert(args is SfmlFontLoaderArgs, INVALID_INITIALIZER_ARGS.Format(nameof(SfmlFontInitializer), nameof(SfmlFontLoaderArgs)));
            return new Font(assetLoader.GetString(args.Key));
        }

        public bool Validate(IAssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".ttf");
        }
    }
}
