using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using Vector2f = SFML.System.Vector2f;

namespace Annex.Sfml.Graphics.Transforms
{
    internal class AnnexSprite : Drawable
    {
        public Texture Texture { get; set; }
        private Transform _transform = Transform.Identity;
        private VertexArray _quads = new VertexArray(PrimitiveType.Quads, 4);

        public AnnexSprite(Texture texture) {
            this.Texture = texture;
        }

        public void Update(TextureContext textureContext) {
            this._transform = new Transform();

            Color color = textureContext.RenderColor.ToSFML(KnownColor.White);
            uint sourceTop = (uint?)textureContext.SourceTextureRect?.Top ?? 0;
            uint sourceLeft = (uint?)textureContext.SourceTextureRect?.Left ?? 0;
            uint sourceWidth = (uint?)textureContext.SourceTextureRect?.Width ?? this.Texture.Size.X;
            uint sourceHeight = (uint?)textureContext.SourceTextureRect?.Height ?? this.Texture.Size.Y;

            var topLeftPos = new Vector2f(textureContext.RenderPosition.X, textureContext.RenderPosition.Y);
            var bottomLeftPos = new Vector2f(textureContext.RenderPosition.X, textureContext.RenderPosition.Y + sourceHeight);
            var bottomRightPos = new Vector2f(textureContext.RenderPosition.X + sourceWidth, textureContext.RenderPosition.Y + sourceHeight);
            var topRightPos = new Vector2f(textureContext.RenderPosition.X + sourceWidth, textureContext.RenderPosition.Y);

            this._quads[0] = new Vertex(topLeftPos, color, new Vector2f(sourceLeft, sourceTop));
            this._quads[1] = new Vertex(bottomLeftPos, color, new Vector2f(sourceLeft, sourceTop + sourceHeight));
            this._quads[2] = new Vertex(bottomRightPos, color, new Vector2f(sourceLeft + sourceWidth, sourceTop + sourceHeight));
            this._quads[3] = new Vertex(topRightPos, color, new Vector2f(sourceLeft + sourceWidth, sourceTop));

            this._transform = Transform.Identity;
        }

        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= this._transform;
            states.Texture = this.Texture;
            target.Draw(this._quads, states);
        }
    }
}
