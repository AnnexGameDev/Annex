using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Services;

namespace AnnexSinglePlayer.Assets
{
    public class TextureManager : AssetManager, ITextureManager
    {
        public TextureManager() : base(new FileSystemStreamer("textures", ".png", ".jpg")) {
        }
    }
}
