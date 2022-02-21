using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace SampleProject.Models
{
    public class Player : IDrawable
    {
        public readonly Vector2f Position;

        private readonly TextureContext _sprite;
        //private readonly TextContext _hoverText;

        //public readonly String Name;

        public Player() {
            //this.Position = Vector.Create(0, 0);
            //this.Name = "Player Name";

            this._sprite = new TextureContext("player.png") {
                RenderPosition = this.Position
            };

            //this._sprite = new SpriteSheetContext("player.png", 4, 4) {
            //    RenderPosition = new OffsetVector(this.Position, Vector.Create(-48, -90))
            //};
            //this._hoverText = new TextContext(this.Name, "Augusta.ttf") {
            //    RenderPosition = new OffsetVector(this.Position, Vector.Create(-48, -100)),
            //    Alignment = new TextAlignment() {
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        Size = Vector.Create(96, 0)
            //    },
            //    FontColor = RGBA.White,
            //    BorderColor = RGBA.Black,
            //    BorderThickness = 3,
            //    FontSize = 16
            //};
        }


        //internal void Animate() {
        //    this._sprite.StepColumn();
        //}

        public void Draw(ICanvas canvas) {
            canvas.Draw(this._sprite);
            //canvas.Draw(this._hoverText);
        }

        public void Dispose() {
            this._sprite.Dispose();
        }
    }
}