namespace Annex.Core.Graphics.Contexts
{
    public class TextureContext : Context
    {
        public string TextureId { get; }

        public TextureContext(string textureId) {
            this.TextureId = textureId;
        }
    }
}