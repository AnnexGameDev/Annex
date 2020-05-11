#nullable enable
using Annex.Resources;

namespace Annex.Graphics.Sfml
{
    public class SfmlFontLoaderArgs : IResourceLoaderArgs
    {
        public string Key { get; set; }
        
        public SfmlFontLoaderArgs(string value) {
            this.Key = value;
        }
    }
}
