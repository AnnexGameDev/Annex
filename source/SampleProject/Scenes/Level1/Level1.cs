using Annex.Core;
using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Collections.Generic;
using Annex.Core.Data;
using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Helpers;
using Annex.Core.Scenes.Components;
using SampleProject.Models;
using SampleProject.Scenes.Level1.Events;
using System.Linq;

namespace SampleProject.Scenes.Level1
{
    public class Level1 : Scene
    {
        private readonly GrassyPlain _grassyPlain;
        private readonly Player _player;
        private readonly IBroadcast<RequestStopAppMessage> _requestStopAppMessage;

        public SolidRectangleContext UIElement { get; }
        public BatchTextureContext Batch { get; }

        public Level1(IBroadcast<RequestStopAppMessage> requestStopAppMessage, IGraphicsService graphicsService) {

            HtmlSceneLoaderHelper.Load("level1.html", this);

            this._requestStopAppMessage = requestStopAppMessage;
            this._player = new Player();
            this._grassyPlain = new GrassyPlain();

            var mainWindow = graphicsService.GetWindow("MainWindow");
            this.Events.Add(CoreEventPriority.UserInput, new PlayerMovementEvent(this._player, mainWindow, 10));
            this.Events.Add(CoreEventPriority.Graphics, new PlayerAnimationEvent(this._player, 500));
            var camera = mainWindow.GetCamera(CameraId.Default);
            camera.Center = this._player.Position;
            camera.Rotation = this._player.Rotation;

            this.UIElement = new SolidRectangleContext(KnownColor.Purple, new Vector2f(0, 0), new Vector2f(300, 200)) {
                Camera = CameraId.UI.ToString(),
                BorderThickness = 5,
                BorderColor = KnownColor.Blue,
            };

            var positions = Collection.Create<object>(4).Indicies(i => i * (float)100);
            var allPossiblePositions = Collection.Permute(positions, positions, (a, b) => (a, b));

            var rects = Collection.Create<object>(4).Indicies(i => i * 96);
            var allRects = Collection.Permute(rects, rects, (a, b) => (a, b, 96, 96));

            this.Batch = new BatchTextureContext("sprites/player.png", allPossiblePositions.ToArray(), Updatability.NeverUpdates) {
                RenderSizes = Collection.Create<(float, float)>(16, (50, 50)).ToArray(),
                RenderOffsets = Collection.Create<(float, float)>(16, (-25, -25)).ToArray(),
                Camera = CameraId.Default.ToString(),
                RenderColors = Collection.Create<RGBA>(16, KnownColor.Red).ToArray(),
                SourceTextureRects = allRects.ToArray(),
                Rotations = Collection.Create<float>(16).Indicies(i => i * (float)25).ToArray(),
            };
        }

        public override void OnWindowClosed(IWindow window) {
            this._requestStopAppMessage.Publish(this, new RequestStopAppMessage());
        }

        protected override void DrawInternal(ICanvas canvas) {
            base.DrawInternal(canvas);
            this._grassyPlain.Draw(canvas);
            this._player.Draw(canvas);
            canvas.Draw(this.Batch);
        }
    }
}