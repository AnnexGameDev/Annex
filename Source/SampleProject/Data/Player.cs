using Annex.Data;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace SampleProject.Data
{
    public class Player : IDrawableObject
    {
        private SurfaceContext _sprite;
        private TextContext _hoverName;
        public Vector2f Position;
        public Vector2f Size;

        public Player() {
            this.Position = new Vector2f(300, 300);
            this.Size = new Vector2f(150, 150);
            this._sprite = new SurfaceContext("dragon.png") {
                SourceSurfaceRect = new IntRect(0, 0, 384 / 4, 384 / 4),
                RenderSize = this.Size,
                RenderPosition = this.Position
            };
            this._hoverName = new TextContext("Dragon Player", "Augusta.ttf") {
                RenderPosition = this.Position,
                Alignment = new TextAlignment() {
                    Size = new Vector2f(this.Size.X, - 26),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom
                },
                FontColor = new RGBA(255, 255, 255),
                FontSize = 26
            };
        }

        public void Draw(IDrawableContext surfaceContext) {
            surfaceContext.Draw(this._sprite);
            surfaceContext.Draw(this._hoverName);
        }
    }
}
