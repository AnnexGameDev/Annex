#nullable enable
using Annex.Data.Shared;
using Annex.Resources;

namespace Annex.Graphics.Sfml
{
    public class SfmlTextureLoaderArgs : IResourceLoaderArgs
    {
        public string Key { get; set; }

        public SfmlTextureLoaderArgs(String key) {
            this.Key = key;
        }
    }
}
