using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace SampleProject.Models
{
    public class Player : IDrawableObject
    {
        public readonly Vector Position;

        private readonly TextureContext _sprite;
        private readonly TextContext _hoverText;

        public readonly String Name;

        public Player() {
            this.Position = new Vector(0, 0);
            this.Name = "Player Name";

            this._sprite = new TextureContext("player.png") {
                SourceTextureRect = new IntRect(0, 0, 96, 96),
                RenderPosition = new OffsetVector(this.Position, new Vector(-48, -90))
            };
            this._hoverText = new TextContext(this.Name, "Augusta.ttf") {
                RenderPosition = new OffsetVector(this.Position, new Vector(-48, -100)),
                Alignment = new TextAlignment() {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Size = new Vector(96, 0)
                },
                FontColor = RGBA.White,
                BorderColor = RGBA.Black,
                BorderThickness = 3,
                FontSize = 16
            };
        }

        public void Draw(IDrawableContext context) {
            context.Draw(this._sprite);
            context.Draw(this._hoverText);
        }
    }
}