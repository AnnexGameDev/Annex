using Annex.Data;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace SampleProject.Data
{
    public class Player : IDrawableObject
    {
        private SurfaceContext _sprite;
        public Vector2f Position;

        public Player() {
            this.Position = new Vector2f();
            this._sprite = new SurfaceContext("dragon.png") {
                SourceSurfaceRect = new IntRect(0, 0, 384 / 4, 384 / 4),
                RenderSize = new Vector2f(150, 150),
                RenderPosition = Position
            };
        }

        public void Draw(IDrawableContext surfaceContext) {
            surfaceContext.Draw(this._sprite);
        }
    }
}
