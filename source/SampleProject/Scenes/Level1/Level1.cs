﻿using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;
using SampleProject.Models;

namespace SampleProject.Scenes.Level1
{
    public class Level1 : Scene
    {
        private readonly GrassyPlain _grassyPlain;
        private readonly Player _player;

        public Level1() {
            this._grassyPlain = new GrassyPlain();
            this._player = new Player();

            var camera = ServiceProvider.Canvas.GetCamera();
            camera.Follow(this._player.Position);

            this.Events.AddEvent(PriorityType.INPUT, this.HandlePlayerInput, 10);
        }

        private void HandlePlayerInput(GameEventArgs args) {
            var ctx = ServiceProvider.Canvas;

            float speed = 1;
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

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {

            if (e.Key == KeyboardKey.Tilde) {
                Debug.ToggleDebugOverlay();
            } else {
                base.HandleKeyboardKeyPressed(e);
            }
        }

        public override void HandleCloseButtonPressed() {
            ServiceProvider.SceneService.LoadGameClosingScene();
        }

        public override void Draw(ICanvas canvas) {
            this._grassyPlain.Draw(canvas);
            this._player.Draw(canvas);

            base.Draw(canvas);
        }
    }
}