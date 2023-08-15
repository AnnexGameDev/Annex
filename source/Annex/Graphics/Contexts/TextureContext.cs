using Annex_Old.Data;
using Annex_Old.Data.Shared;

namespace Annex_Old.Graphics.Contexts
{
    public class TextureContext : DrawingContext
    {
        public String SourceTextureName { get; set; }
        public Vector RenderPosition { get; set; }
        public Vector? RenderSize { get; set; }
        public IntRect? SourceTextureRect { get; set; }
        public RGBA? RenderColor { get; set; }
        public float Rotation { get; set; }
        public Vector RelativeRotationOrigin { get; set; }

        internal ITexturePlatformTarget? Target;

        public TextureContext(String textureName) {
            this.SourceTextureName = textureName;
            this.RenderPosition = Vector.Create();
            this.RenderSize = null;
            this.SourceTextureRect = null;
            this.Rotation = 0;
            this.RenderColor = null;
            this.RelativeRotationOrigin = Vector.Create();
        }
    }
}
