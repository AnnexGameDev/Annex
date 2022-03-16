using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts
{
    public class TextureContext : DrawContext
    {
        public Shared<string> TextureId { get; }
        public Vector2f RenderPosition { get; }
        public Vector2f? RenderSize { get; init; }
        public IntRect? SourceTextureRect { get; init; }
        public RGBA? RenderColor { get; init; }
        public Shared<float>? Rotation { get; init; }
        public Vector2f? RelativeRotationOrigin { get; init; }

        public TextureContext(Shared<string> textureId) : this(textureId, new Vector2f()) {

        }

        public TextureContext(Shared<string> textureId, Vector2f renderPosition) {
            this.TextureId = textureId;
            this.RenderPosition = renderPosition;
            this.RenderSize = null;
            this.SourceTextureRect = null;
            this.RenderColor = null;
            this.Rotation = null;
            this.RelativeRotationOrigin = null;
        }
    }
}