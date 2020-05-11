using Annex.Resources;
using SFML.Graphics;
using System.IO;

namespace Annex.Graphics.Sfml
{
    public class SfmlFontLoader : IResourceLoader
    {
        public readonly string ResourcePath;

        public SfmlFontLoader(string path) {
            this.ResourcePath = path;
        }

        public object Load(IResourceLoaderArgs args, IDataLoader resourceLoader) {
            Debug.Assert(args is SfmlFontLoaderArgs, $"{nameof(SfmlFontLoader)} requires {nameof(SfmlFontLoaderArgs)} args");
            return new Font(resourceLoader.GetString(args.Key));
        }

        public bool Validate(IResourceLoaderArgs args) {
            args.Key = Path.Combine(this.ResourcePath, args.Key);
            return args.Key.EndsWith(".ttf");
        }
    }
}
