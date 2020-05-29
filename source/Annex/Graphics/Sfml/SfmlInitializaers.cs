using Annex.Assets;
using SFML.Graphics;
using System.IO;

namespace Annex.Graphics.Sfml
{
    public class SfmlTextureInitializer : IAssetInitializer
    {
        public string AssetPath { get; set; }

        public SfmlTextureInitializer(string path) {
            this.AssetPath = path;
        }

        public object Load(AssetInitializerArgs args, IAssetLoader assetLoader) {
            return new Texture(assetLoader.GetString(args.Key));
        }

        public bool Validate(AssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".png");
        }
    }

    public class SfmlFontInitializer : IAssetInitializer
    {
        public string AssetPath { get; set; }

        public SfmlFontInitializer(string path) {
            this.AssetPath = path;
        }

        public void CopyAssetToBinary(string path, IAssetLoader assetLoader) {
            throw new System.NotImplementedException();
        }

        public object Load(AssetInitializerArgs args, IAssetLoader assetLoader) {
            return new Font(assetLoader.GetString(args.Key));
        }

        public bool Validate(AssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".ttf");
        }
    }

    public class SfmlIconInitializer : IAssetInitializer
    {
        public string AssetPath { get; set; }

        public SfmlIconInitializer(string path) {
            this.AssetPath = path;
        }

        public object Load(AssetInitializerArgs args, IAssetLoader assetLoader) {
            return new Image(assetLoader.GetString(args.Key));
        }

        public bool Validate(AssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".png");
        }
    }
}
