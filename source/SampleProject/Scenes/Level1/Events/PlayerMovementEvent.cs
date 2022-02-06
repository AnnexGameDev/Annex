using SampleProject.Models;

namespace SampleProject.Scenes.Level1.Events
{
    public class PlayerMovementEvent : GameEvent
    {
        private readonly Player _player;

        public PlayerMovementEvent(Player player, int interval_ms) : base(interval_ms, 0) {
            this._player = player;
        }

        protected override void Run(EventArgs gameEventArgs) {
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
