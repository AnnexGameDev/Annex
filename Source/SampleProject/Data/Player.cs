using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace SampleProject.Data
{
    public class Player : Entity, IDrawableObject
    {
        public readonly String PlayerName;
        public readonly String PlayerSprite;

        private readonly SurfaceContext _sprite;
        private readonly TextContext _hoverName;
        private readonly String _hoverNameFont;

        public Player() {
            this.PlayerName = new String("Dragon Player");
            this.PlayerSprite = new String("dragon.png");
            this._hoverNameFont = new String("Augusta.ttf");

            this.EntityPosition.Set(300, 300);
            this.EntitySize.Set(150, 150);
            this._sprite = new SurfaceContext(this.PlayerSprite) {
                SourceSurfaceRect = new IntRect(0, 0, 384 / 4, 384 / 4),
                RenderSize = this.EntitySize,
                RenderPosition = this.EntityPosition
            };
            this._hoverName = new TextContext(this.PlayerName, this._hoverNameFont) {
                RenderPosition = this.EntityPosition,
                Alignment = new TextAlignment() {
                    Size = new OffsetScalingVector(this.EntitySize, new Vector(0, -26), new Vector(1, 0)),
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
