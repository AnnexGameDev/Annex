using Annex;
using Annex.Events;
using Annex.Scenes;
using SampleProject.Models;

namespace SampleProject.Scenes.Level1.Events
{
    public class PlayerInputEvent : CustomEvent
    {
        private readonly Player _player;

        public PlayerInputEvent(Player player, int interval_ms, string eventID = "", int delay_ms = 0) : base(eventID, interval_ms, delay_ms) {
            this._player = player;
        }

        protected override void Run(EventArgs args) {
            var ctx = ServiceProvider.Canvas;

            float speed = 1;
            if (!ctx.IsActive) {
                return;
            }
            if (ctx.IsKeyDown(KeyboardKey.Up)) {
                this._player.Position.Y -= speed;
            }
            if (ctx.IsKeyDown(KeyboardKey.Down)) {
                this._player.Position.Y += speed;
            }
            if (ctx.IsKeyDown(KeyboardKey.Left)) {
                this._player.Position.X -= speed;
            }
            if (ctx.IsKeyDown(KeyboardKey.Right)) {
                this._player.Position.X += speed;
            }
        }
    }
}
