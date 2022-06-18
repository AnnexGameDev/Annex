using Annex.Core.Assets;
using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Events;
using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Helpers;
using Annex.Core.Input;
using Annex.Core.Input.Platforms;
using Annex.Core.Scenes;
using Annex.Core.Scenes.Components;
using Annex.Core.Scenes.Layouts.Html;
using Annex.Core.Time;
using Scaffold;
using Scaffold.DependencyInjection;
using Scaffold.Logging;

namespace Annex.Core;

public abstract class AnnexApp : ScaffoldApp
{
    public void Run<TStartingScene>() where TStartingScene : IScene  {
        try {
            var sceneService = this.Container.Resolve<ISceneService>();
            var eventService = this.Container.Resolve<ICoreEventService>();
            var graphicsService = this.Container.Resolve<IGraphicsService>();
            var assetService = this.Container.Resolve<IAssetService>();

            this.Container.Resolve<ClipboardHelper>();
            this.Container.Resolve<GraphicsEngineHelper>();
            this.Container.Resolve<GameTimeHelper>();
            this.Container.Resolve<KeyboardHelper>();
            this.Container.Resolve<HtmlSceneLoaderHelper>();

            this.SetupAssetBundles(assetService);
            this.CreateWindow(graphicsService, assetService);
            sceneService.LoadScene<TStartingScene>();
            eventService.Run();
        }
        catch (Exception ex) {
            Log.Trace(LogSeverity.Error, "Exception in main gameloop", ex);
        }
    }

    protected override void RegisterTypes(IContainer container) {
        base.RegisterTypes(container);

        container.Register<IHtmlSceneLoader, HtmlSceneLoader>();
        container.RegisterSingleton<ICoreEventService, CoreEventService>();
        container.Register<IInputService, InputService>();
        container.RegisterSingleton<ITimeService, StopwatchTimeService>();
        container.RegisterSingleton<ISceneService, SceneService>();
        container.RegisterSingleton<IGraphicsService, GraphicsService>();

        container.RegisterAssetGroup(KnownAssetGroups.TextureGroupId);
        container.RegisterAssetGroup(KnownAssetGroups.FontGroupId);
        container.RegisterAssetGroup(KnownAssetGroups.SceneDataGroupId);
        container.RegisterSingleton<IAssetService, AssetService>();

        container.Register<IPriorityEventQueue, PriorityEventQueue>();
        container.RegisterBroadcast<RequestStopAppMessage>();

#if WINDOWS
        container.Register<IPlatformKeyboardService, WindowsKeyboardService>();
#endif
    }

    protected abstract void CreateWindow(IGraphicsService graphicsService, IAssetService assetService);
    protected abstract void SetupAssetBundles(IAssetService assetService);
}
