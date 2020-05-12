using Annex.Assets;

namespace Annex.Graphics.Sfml
{
    public class SfmlFontConverter : IAssetInitializer
    {
        public string AssetPath { get; set; }

        public void CopyAssetToBinary(string path, IAssetLoader assetLoader) {
            this.AssetPath = path;
            throw new System.NotImplementedException();
        }

        public object Load(IAssetInitializerArgs args, IAssetLoader assetLoader) {
            throw new System.NotImplementedException();
        }

        public bool Validate(IAssetInitializerArgs key) {
            throw new System.NotImplementedException();
        }
    }
}
