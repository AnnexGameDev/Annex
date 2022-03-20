using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace SampleProject.Models
{
    public class Player : IDrawable
    {
        public readonly Vector2f Position;

        private readonly DrawContext _sprite;
        public readonly Vector2f Size = new Vector2f(192, 192);
        public Shared<float> Rotation = new Shared<float>(0);
        //private readonly TextContext _hoverText;

        //public readonly String Name;

        public Player() {
            this.Position = new Vector2f(960 / 2, 640 / 2);
            //this.Name = "Player Name";

            this._sprite = new TextureContext("player.png", this.Position) {
                SourceTextureRect = new IntRect(0, 0, 96, 96),
                RenderColor = KnownColor.Red,
                RenderSize = Size,
                Rotation = Rotation,
                RenderOffset = new Vector2f(-0.5f, -0.5f)
            };
            this._sprite = new SpritesheetContext("player.png", this.Position, 4, 4) {
                RenderColor = KnownColor.Red,
                RenderSize = this.Size,
                Rotation = this.Rotation,
                RenderOffset = new Vector2f(-0.5f, -0.5f)
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