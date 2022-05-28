using Annex_Old.Core.Events;
using Annex_Old.Core.Graphics.Windows;
using Annex_Old.Core.Input;
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

            if (window.IsKeyDown(KeyboardKey.E)) {
                this._player.Size.Scale(1.1f);
            }

            if (window.IsKeyDown(KeyboardKey.Q)) {
                this._player.Size.Scale(0.9f);
            }

            if (window.IsKeyDown(KeyboardKey.W)) {
                this._player.Rotation.Set(this._player.Rotation.Value + 1);
            }

            if (window.IsKeyDown(KeyboardKey.S)) {
                this._player.Rotation.Set(this._player.Rotation.Value - 1);
            }
        }
    }
}
