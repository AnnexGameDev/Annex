#nullable enable
using Annex.Data;
using Annex.Data.Shared;

namespace Annex.Graphics.Contexts
{
    public class TextureContext
    {
        public String SourceTextureName { get; set; }
        public Vector RenderPosition { get; set; }
<<<<<<< HEAD
        public Vector? RenderSize { get; set; }
=======
        public Vector RenderSize { get; set; }
>>>>>>> 1681279fd3c0b78685fe137155ae535bbe391b02
        public IntRect? SourceTextureRect { get; set; }
        public RGBA? RenderColor { get; set; }
        public float Rotation { get; set; }
        public Vector RelativeRotationOrigin { get; set; }
        public bool UseUIView { get; set; }

        public TextureContext(String textureName) {
            this.SourceTextureName = textureName;
            this.RenderPosition = new Vector();
<<<<<<< HEAD
            this.RenderSize = null;
=======
            this.RenderSize = new Vector();
>>>>>>> 1681279fd3c0b78685fe137155ae535bbe391b02
            this.SourceTextureRect = null;
            this.Rotation = 0;
            this.UseUIView = false;
            this.RenderColor = null;
            this.RelativeRotationOrigin = new Vector();
        }
    }
}
