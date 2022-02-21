using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts
{
    public class TextureContext : DrawContext
    {
        public string TextureId { get; }
        public Vector2f RenderPosition { get; init; }
        public Vector2f? RenderSize { get; init; }
        public IntRect? SourceTextureRect { get; set; }
        public RGBA? RenderColor { get; set; }
        public float Rotation { get; set; }
        public Vector2f? RelativeRotationOrigin { get; set; }

        public TextureContext(string textureId) {
            this.TextureId = textureId;
            this.RenderPosition = new Vector2f();
            this.RenderSize = null;
            this.SourceTextureRect = null;
            this.RenderColor = null;
            this.Rotation = 0;
            this.RelativeRotationOrigin = null;
        }
    }
}