using Annex_Old.Core.Assets;
using Annex_Old.Core.Broadcasts;
using Annex_Old.Core.Broadcasts.Messages;
using Annex_Old.Core.Events;
using Annex_Old.Core.Events.Core;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Helpers;
using Annex_Old.Core.Input;
using Annex_Old.Core.Input.Platforms;
using Annex_Old.Core.Scenes;
using Annex_Old.Core.Scenes.Components;
using Annex_Old.Core.Scenes.Layouts.Html;
using Annex_Old.Core.Time;
using Scaffold;
using Scaffold.DependencyInjection;
using Scaffold.Logging;

namespace Annex_Old.Core;

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
        container.RegisterSingleton<IAssetService, AssetService>();
        container.Register<IAssetGroup, AssetGroup>();
        container.Register<IPriorityEventQueue, PriorityEventQueue>();
        container.RegisterBroadcast<RequestStopAppMessage>();

#if WINDOWS
        container.Register<IPlatformKeyboardService, WindowsKeyboardService>();
#endif
    }

    protected abstract void CreateWindow(IGraphicsService graphicsService, IAssetService assetService);
    protected abstract void SetupAssetBundles(IAssetService assetService);
}
