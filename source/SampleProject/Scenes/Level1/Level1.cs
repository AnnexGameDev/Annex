using Annex.Events;
using Annex.Graphics;
using Annex.Scenes.Components;
using SampleProject.Models;
using SampleProject.Scenes.Level1.Events;

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

            this.Events.AddEvent(PriorityType.INPUT, new PlayerMovementEvent(this._player, 10));
            this.Events.AddEvent(PriorityType.ANIMATION, new PlayerAnimationEvent(this._player, 500));
        }

        public override void HandleCloseButtonPressed() {
            Game.Terminate();
        }

        public override void Draw(ICanvas canvas) {
            this._grassyPlain.Draw(canvas);
            this._player.Draw(canvas);

            base.Draw(canvas);
        }
    }
}