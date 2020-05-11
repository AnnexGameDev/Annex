using Annex.Resources;
using SFML.Graphics;
using System.IO;

namespace Annex.Graphics.Sfml
{
    public class SfmlTextureLoader : IResourceLoader
    {
        public readonly string ResourcePath;

        public SfmlTextureLoader(string path) {
            this.ResourcePath = path;
        }

        public object Load(IResourceLoaderArgs args, IDataLoader resourceLoader) {
            Debug.Assert(args is SfmlTextureLoaderArgs, $"{nameof(SfmlTextureLoader)} requires {nameof(SfmlTextureLoaderArgs)} args");
            return new Texture(resourceLoader.GetString(args.Key));
        }

        public bool Validate(IResourceLoaderArgs args) {
            args.Key = Path.Combine(this.ResourcePath, args.Key);
            return args.Key.EndsWith(".png");
        }
    }
}
