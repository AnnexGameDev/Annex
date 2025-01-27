using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Assets.Bundles;
using Annex.Core.Events;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Core.Networking;
using Annex.Core.Networking.Engines.DotNet;
using Annex.Core.Time;
using Annex.Sfml.Graphics;
using SampleProject.Scenes.ListViewExample;
using Scaffold.Collections;
using Scaffold.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject;

public class Game : AnnexApp
{
    private static bool _stop = false;
    public static readonly IList<IGameEvent> Events = new ConcurrentList<IGameEvent>();

    public static Guid MainWindowId { get; private set; }

    internal static void Stop()
    {
        _stop = true;
    }

    private static async Task Main(string[] args)
    {
#if !DEBUG
        try {
#endif
        using var game = new Game();
        await game.StartAsync();
#if !DEBUG
        } catch (Exception e) {
            Log.Trace(LogSeverity.Error, "Exception in main", exception: e);
        }
#endif
    }

    private async Task StartAsync()
    {
        var assetService = Container.Resolve<IAssetService>();
        var graphicsService = Container.Resolve<IGraphicsService>();
        var timeService = Container.Resolve<ITimeService>();


        SetupAssetBundles(assetService);
        var window = CreateWindow(graphicsService, assetService);
        Events.Add(new GameEvent(timeService, window.DrawCurrentSceneAsync, 16));

        window.LoadScene<ListViewExampleScene>();

        while (!_stop)
        {
            foreach (var e in Events)
            {
                await e.ProbeAsync();
            }
            Thread.Sleep(0);
        }
    }

    protected IWindow CreateWindow(IGraphicsService graphicsService, IAssetService assetService)
    {
        var window = graphicsService.CreateWindow("Main Window", 960, 640, WindowStyle.Default);
        window.IsVisible = true;

        Game.MainWindowId = window.Id;

        var textures = assetService.Textures();

        var icon = textures.GetAsset("icons/icon.png")!;
        var cursor = textures.GetAsset("cursors/cursor.png")!;

        window.SetIcon(100, 100, icon);
        window.SetMouseImage(cursor, 16, 16, 0, 0);
        return window;
    }

    protected override void RegisterTypes(IContainer container)
    {
        base.RegisterTypes(container);

        //Log.SetSeverityEnabled(LogSeverity.Verbose, false);

        var asSingleton = new RegistrationOptions() { Singleton = true };
        var asAggregate = new RegistrationOptions() { Aggregate = true };

        container.Register<INetworkingEngine, DotNetNetworkingEngine>(asSingleton);
        container.Register<IGraphicsEngine, SfmlGraphicsEngine>(asSingleton);
        container.Register<ListViewExampleScene>();

        //container.Register<IPacketHandler, SimpleMessagePacketHandler>(asAggregate);
        //container.Register<IPacketHandler, SimpleRequestPacketHandler>(asAggregate);
    }

    protected void SetupAssetBundles(IAssetService assetService)
    {
        string assetRoot = GetAssetRoot();
        var textures = assetService.Textures();
        var fonts = assetService.Fonts();
        var sceneData = assetService.SceneData();

#if DEBUG
        string? textureRoot = Path.Combine(assetRoot, "textures");
        string? fontsRoot = Path.Combine(assetRoot, "fonts");
        string? sceneDataRoot = Path.Combine(assetRoot, "scenes");
#else
        string? textureRoot = null;
        string? fontsRoot = null;
        string? sceneDataRoot = null;
#endif
        textures.AddBundle(new PakFileBundle("textures.pak", "*.png", textureRoot));
        fonts.AddBundle(new PakFileBundle("fonts.pak", "*.ttf", fontsRoot));
        sceneData.AddBundle(new FileSystemBundle("*.html", sceneDataRoot));
    }

    private string GetAssetRoot()
    {
#if DEBUG
        var root = Paths.GetParentFolderWithFile("Annex.sln");
#else
        var root = Paths.ApplicationPath;
#endif
        return Path.Combine(root, "assets");
    }
}
