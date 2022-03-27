using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace SampleProject.Models
{
    public class Player : IDrawable
    {
        public readonly Vector2f Position;

        private readonly DrawContext _sprite;
        public readonly Vector2f Size = new Vector2f(50, 50);
        public Shared<float> Rotation = new Shared<float>(0);
        private readonly TextContext _hoverText;

        public readonly Shared<string> Name;

        public Player() {
            this.Position = new Vector2f(960 / 2, 640 / 2);
            this.Name = "Player Name";

            this._sprite = new SpritesheetContext("sprites/player.png", this.Position, 4, 4) {
                RenderColor = KnownColor.Red,
                RenderSize = this.Size,
                Rotation = this.Rotation,
                RenderOffset = new ScalingVector2f(this.Size, new Vector2f(-0.5f, -1f))
            };

            this._hoverText = new TextContext(this.Name, "lato.ttf") {
                Position = this.Position,
                PositionOffset = new ScalingVector2f(this.Size, new Vector2f(0, -1f)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Color = KnownColor.White,
                BorderColor = KnownColor.Black,
                BorderThickness = 3,
                FontSize = 16,
                Rotation = this.Rotation
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
            canvas.Draw(this._hoverText);
        }

        public void Dispose() {
            this._sprite.Dispose();
            this._hoverText.Dispose();
        }
    }
}