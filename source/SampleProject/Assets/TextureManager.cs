namespace SampleProject.Assets
{
    public class TextureManager : AssetManager, ITextureManager
    {
        public TextureManager() : base(new PakFileStreamer("textures", ".png", ".jpg")) {
        }
    }
}
