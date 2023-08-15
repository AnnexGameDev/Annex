using SFML.Graphics;

namespace Annex_Old.Graphics.Sfml
{
    internal class DrawableVertexArray : Transformable, Drawable
    {
        private readonly Texture _texture;
        private readonly VertexArray _vertices;

        public DrawableVertexArray(VertexArray vertices, Texture texture) {
            this._texture = texture;
            this._vertices = vertices;
        }

        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= Transform;
            states.Texture = _texture;
            target.Draw(_vertices, states);
        }
    }
}
