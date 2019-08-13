using Annex.Data;
using Annex.Data.Shared;

namespace Annex.Graphics.Contexts
{
    public class TextureContext
    {
        public String SourceTextureName { get; set; }
        public Vector RenderPosition { get; set; }
        public Vector RenderSize { get; set; }
        public IntRect? SourceTextureRect { get; set; }
        public RGBA? RenderColor { get; set; }
        public float Rotation { get; set; }
        public Vector RelativeRotationOrigin { get; set; }
        public bool UseUIView { get; set; }

        public TextureContext(String textureName) {
            this.SourceTextureName = textureName;
            this.RenderPosition = new Vector();
            this.RenderSize = new Vector();
            this.SourceTextureRect = null;
            this.Rotation = 0;
            this.UseUIView = false;
            this.RenderColor = null;
            this.RelativeRotationOrigin = new Vector();
        }
    }
}
