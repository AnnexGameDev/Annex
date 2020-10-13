using Annex.Assets;
using Annex.Assets.Services;
using Annex.Assets.Streams;

namespace SampleProject.Assets
{
    public class TextureManager : AssetManager, ITextureManager
    {
        public TextureManager() : base(new PakFileStreamer("textures", ".png", ".jpg")) {
        }
    }
}
