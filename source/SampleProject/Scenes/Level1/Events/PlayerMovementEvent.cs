using Annex.Core.Events;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input;
using SampleProject.Models;

namespace SampleProject.Scenes.Level1.Events
{
    public class PlayerMovementEvent : Event
    {
        private readonly Player _player;
        private readonly IWindow _window;

        public PlayerMovementEvent(Player player, IWindow window, int interval_ms) : base(interval_ms, 0) {
            this._player = player;
            this._window = window;
        }

        protected override void Run() {
            var window = this._window;

            float speed = 1;
            if (window.IsKeyDown(KeyboardKey.Up)) {
                this._player.Position.Y -= speed;
            }
            if (window.IsKeyDown(KeyboardKey.Down)) {
                this._player.Position.Y += speed;
            }
            if (window.IsKeyDown(KeyboardKey.Left)) {
                this._player.Position.X -= speed;
            }
            if (window.IsKeyDown(KeyboardKey.Right)) {
                this._player.Position.X += speed;
            }
        }
    }
}
