using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Services;
using Annex.Assets.Streams;

namespace SampleProject.Assets
{
    public class TextureManager : AssetManager, ITextureManager
    {
        public TextureManager() : base(
            new AESEncryptionStreamer("bababoee",
            new FileSystemStreamer("textures", ".png", ".jpg")
            )) {
        }
    }
}
