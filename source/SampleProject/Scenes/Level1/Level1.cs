using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Data;
using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Scenes.Components;
using SampleProject.Models;
using SampleProject.Scenes.Level1.Events;

namespace SampleProject.Scenes.Level1
{
    public class Level1 : Scene //: HtmlScene
    {
        private readonly GrassyPlain _grassyPlain;
        private readonly Player _player;
        private readonly IBroadcast<RequestStopAppMessage> _requestStopAppMessage;

        public SolidRectangleContext UIElement { get; }

        public Level1(IBroadcast<RequestStopAppMessage> requestStopAppMessage, IGraphicsService graphicsService) {
            this._requestStopAppMessage = requestStopAppMessage;
            this._player = new Player();
            this._grassyPlain = new GrassyPlain();

            var mainWindow = graphicsService.GetWindow("MainWindow");
            this.Events.Add(CoreEventPriority.UserInput, new PlayerMovementEvent(this._player, mainWindow, 10));
            var camera = mainWindow.GetCamera("world");
            camera.Center = this._player.Position;
            camera.Rotation = this._player.Rotation;

            this.UIElement = new SolidRectangleContext(KnownColor.Purple, new Vector2f(0, 0), new Vector2f(300, 200)) {
                Camera = "ui",
                BorderThickness = 5,
                BorderColor = KnownColor.Blue,
            };
        }

        // public Level1() : base("level1.html") {

            //    var camera = ServiceProvider.Canvas.GetCamera();
            //    camera.Follow(this._player.Position);

            //    this.Events.AddEvent(PriorityType.INPUT, new PlayerMovementEvent(this._player, 10));
            //    this.Events.AddEvent(PriorityType.ANIMATION, new PlayerAnimationEvent(this._player, 500));
        //}

        public override void OnWindowClosed(IWindow window) {
            this._requestStopAppMessage.Publish(this, new RequestStopAppMessage());
        }

        public override void Draw(ICanvas canvas) {
            this._grassyPlain.Draw(canvas);
            this._player.Draw(canvas);
            canvas.Draw(this.UIElement);
            base.Draw(canvas);
        }
    }
}