using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Calculations;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;
using Annex.Core.Scenes.Components;
using SampleProject.Models;

namespace SampleProject.Scenes.Level1
{
    public class Level1 : Scene //: HtmlScene
    {
        private readonly GrassyPlain _grassyPlain;
        private readonly Player _player;
        private readonly Player _player2;
        private readonly IBroadcast<RequestStopAppMessage> _requestStopAppMessage;

        public Level1(IBroadcast<RequestStopAppMessage> requestStopAppMessage) {
            this._requestStopAppMessage = requestStopAppMessage;
            this._player = new Player();
            this._player2 = new Player();
        }

        // public Level1() : base("level1.html") {
            //    this._grassyPlain = new GrassyPlain();

            //    var camera = ServiceProvider.Canvas.GetCamera();
            //    camera.Follow(this._player.Position);

            //    this.Events.AddEvent(PriorityType.INPUT, new PlayerMovementEvent(this._player, 10));
            //    this.Events.AddEvent(PriorityType.ANIMATION, new PlayerAnimationEvent(this._player, 500));
        //}

        public override void OnWindowClosed(IWindow window) {
            this._requestStopAppMessage.Publish(this, new RequestStopAppMessage());
        }

        public override void Draw(ICanvas canvas) {
            //this._grassyPlain.Draw(canvas);
            this._player.Draw(canvas);
            this._player2.Draw(canvas);
            base.Draw(canvas);
        }

        public override void OnMouseMoved(IWindow window, MouseMovedEvent mouseMovedEvent) {
            base.OnMouseMoved(window, mouseMovedEvent);

            this._player.Rotation.Set(-Rotation.ComputeRotation(960 / 2, 640 / 2, mouseMovedEvent.WindowX, mouseMovedEvent.WindowY));
        }
    }
}