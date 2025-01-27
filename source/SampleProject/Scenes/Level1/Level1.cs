using Annex.Core;
using Annex.Core.Collections.Generic;
using Annex.Core.Data;
using Annex.Core.Events;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Scenes.Elements;
using Annex.Core.Time;
using SampleProject.Models;
using SampleProject.Scenes.Level1.Events;
using System.Linq;

namespace SampleProject.Scenes.Level1;

public class Level1 : Scene
{
    private readonly GrassyPlain _grassyPlain;
    private readonly Player _player;

    public SolidRectangleContext UIElement { get; }
    public BatchTextureContext Batch { get; }

    public Level1(IGraphicsService graphicsService, ITimeService timeService)
    {
        this._player = new Player();
        this._grassyPlain = new GrassyPlain();

        var mainWindow = graphicsService.GetWindow(Game.MainWindowId);
        Game.Events.Add(new GameEvent(timeService, new PlayerMovementEvent(this._player, mainWindow).ProcessAsync, 10));
        Game.Events.Add(new GameEvent(timeService, new PlayerAnimationEvent(this._player).RunAsync, 500));
        var camera = mainWindow.GetCamera(CameraId.Default);
        camera.Center = this._player.Position;
        camera.Rotation = this._player.Rotation;

        this.UIElement = new SolidRectangleContext(KnownColor.Purple, new Vector2f(0, 0), new Vector2f(300, 200))
        {
            Camera = CameraId.UI.ToString(),
            BorderThickness = 5.0f.ToShared(),
            BorderColor = KnownColor.Blue,
        };

        var positions = Collection.Create<object>(4).Indicies(i => i * (float)100);
        var allPossiblePositions = Collection.Permute(positions, positions, (a, b) => (a, b));

        var rects = Collection.Create<object>(4).Indicies(i => i * 96);
        var allRects = Collection.Permute(rects, rects, (a, b) => (a, b, 96, 96));

        this.Batch = new BatchTextureContext("sprites/player.png", allPossiblePositions.ToArray(), Updatability.NeverUpdates)
        {
            RenderSizes = Collection.Create<(float, float)>(16, (50, 50)).ToArray(),
            RenderOffsets = Collection.Create<(float, float)>(16, (-25, -25)).ToArray(),
            Camera = CameraId.Default.ToString(),
            RenderColors = Collection.Create<RGBA>(16, KnownColor.Red).ToArray(),
            SourceTextureRects = allRects.ToArray(),
            Rotations = Collection.Create<float>(16).Indicies(i => i * (float)25).ToArray(),
        };
    }

    public override void OnWindowClosed(IWindow window)
    {
        Game.Stop();
    }

    protected override void DrawInternal(IWindow window)
    {
        base.DrawInternal(window);
        this._grassyPlain.DrawOn(window);
        this._player.DrawOn(window);
        window.Draw(this.Batch);
    }
}