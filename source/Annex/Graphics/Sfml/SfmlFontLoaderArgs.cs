#nullable enable
using Annex.Assets;

namespace Annex.Graphics.Sfml
{
    public class SfmlFontLoaderArgs : IAssetInitializerArgs
    {
        public string Key { get; set; }
        
        public SfmlFontLoaderArgs(string value) {
            this.Key = value;
        }
    }
}
