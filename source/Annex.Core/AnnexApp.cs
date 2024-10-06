using Annex.Core.Assets;
using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Events;
using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Input;
using Annex.Core.Input.Platforms;
using Annex.Core.Networking;
using Annex.Core.Networking.Packets;
using Annex.Core.Scenes;
using Annex.Core.Scenes.Elements;
using Annex.Core.Scenes.Layouts;
using Annex.Core.Scenes.Layouts.Html;
using Annex.Core.Time;
using Scaffold;
using Scaffold.DependencyInjection;
using Scaffold.Extensions;
using System.Runtime.InteropServices;

namespace Annex.Core;

public abstract class AnnexApp : ScaffoldApp
{
    public Task RunAsync<TStartingScene>() where TStartingScene : IScene {
        var sceneService = this.Container.Resolve<ISceneService>();
        var eventService = this.Container.Resolve<ICoreEventService>();
        var graphicsService = this.Container.Resolve<IGraphicsService>();
        var assetService = this.Container.Resolve<IAssetService>();
        this.Container.Resolve<IPacketHandlerService>().Init(this.Container.Resolve<IEnumerable<IPacketHandler>>());

        this.SetupAssetBundles(assetService);
        this.CreateWindow(graphicsService, assetService);
        sceneService.LoadScene<TStartingScene>();
        return eventService.RunAsync();
    }

    protected override void RegisterTypes(IContainer container) {
        base.RegisterTypes(container);

        container.RegisterAggregate<IUIElementTypeResolver, AnnexUIElementTypeResolver>();
        container.RegisterSingleton<IUIElementTypeResolverService, UIElementTypeResolverService>();
        container.Register<IHtmlSceneLoader, HtmlSceneLoader>();
        container.RegisterSingleton<ICoreEventService, CoreEventService>();
        container.RegisterSingleton<IInputService, InputService>();
        container.RegisterSingleton<ITimeService, StopwatchTimeService>();
        container.RegisterSingleton<ISceneService, SceneService>();
        container.RegisterSingleton<IGraphicsService, GraphicsService>();
        container.RegisterSingleton<IPacketHandlerService, PacketHandlerService>();
        container.RegisterSingleton<IThreadService, ThreadService>();

        container.RegisterAssetGroup(KnownAssetGroups.TextureGroupId);
        container.RegisterAssetGroup(KnownAssetGroups.FontGroupId);
        container.RegisterAssetGroup(KnownAssetGroups.SceneDataGroupId);
        container.RegisterSingleton<IAssetService, AssetService>();

        container.Register<IPriorityEventQueue, PriorityEventQueue>();
        container.RegisterBroadcast<RequestStopAppMessage>();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            container.Register<IPlatformKeyboardService, WindowsKeyboardService>();
        }
    }

    protected abstract void CreateWindow(IGraphicsService graphicsService, IAssetService assetService);
    protected abstract void SetupAssetBundles(IAssetService assetService);
}
