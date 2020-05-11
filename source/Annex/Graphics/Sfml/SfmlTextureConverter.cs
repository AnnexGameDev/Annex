using Annex.Resources;
using SFML.Graphics;

namespace Annex.Graphics.Sfml
{
    public class SfmlTextureConverter : IResourceLoader
    {
        public object Load(IDataLoader resourceLoader, string key, object flag) {
            return new Texture(resourceLoader.GetBytes(key));
        }

        public object Load(IResourceLoaderArgs args, IDataLoader resourceLoader) {
            throw new System.NotImplementedException();
        }

        public bool Validate(IResourceLoaderArgs key) {
            throw new System.NotImplementedException();
        }
    }
}
