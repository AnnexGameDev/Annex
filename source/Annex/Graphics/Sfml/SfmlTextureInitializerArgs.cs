#nullable enable
using Annex.Assets;

namespace Annex.Graphics.Sfml
{
    public class SfmlTextureInitializerArgs : IAssetInitializerArgs
    {
        public string Key { get; set; }

        public SfmlTextureInitializerArgs(string key) {
            this.Key = key;
        }
    }
}
