using Annex.Core.Assets;
using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Input;
using Annex.Core.Input.Platforms;
using Annex.Core.Logging;
using Annex.Core.Scenes;
using Annex.Core.Services;
using Annex.Core.Time;

namespace Annex.Core;

public abstract class AnnexApp
{
    private readonly ISceneService _sceneService;
    private readonly ICoreEventService _eventService;
    private readonly IGraphicsService _graphicsService;
    private readonly IAssetService _assetManager;
    private readonly IContainer _container;

    public AnnexApp() {
        this._container = new Container();

        var asSingleton = new RegistrationOptions() { Singleton = true };
        this._container.Register<ILogService, Log>(asSingleton);
        this._container.Register<ICoreEventService, CoreEventService>(asSingleton);
        this._container.Register<IInputService, InputService>();
        this._container.Register<ITimeService, StopwatchTimeService>(asSingleton);
        this._container.Register<ISceneService, SceneService>(asSingleton);
        this._container.Register<IGraphicsService, GraphicsService>(asSingleton);
        this._container.Register<IAssetService, AssetService>(asSingleton);
        this._container.Register<IAssetGroup, AssetGroup>();
        this._container.RegisterBroadcast<RequestStopAppMessage>();

#if WINDOWS
        this._container.Register<IPlatformKeyboardService, WindowsKeyboardService>();
#endif

        this.RegisterTypes(this._container);

        this._sceneService = this._container.Resolve<ISceneService>();
        this._eventService = this._container.Resolve<ICoreEventService>();
        this._graphicsService = this._container.Resolve<IGraphicsService>();
        this._assetManager = this._container.Resolve<IAssetService>();
    }


    public void Run<TStartingScene>() where TStartingScene : IScene  {
        try {
            this.SetupAssetBundles(this._assetManager);
            this.CreateWindow(this._graphicsService);
            this._sceneService.LoadScene<TStartingScene>();
            this._eventService.Run();
        }
        catch (Exception ex) {
            Log.Trace(LogSeverity.Error, "Exception in main gameloop", ex);
            this._container.Resolve<ILogService>(); // Resolve the log so we have an instance up to persist the log to.
        }
        finally {
            this._container.Dispose();
        }
    }

    protected abstract void RegisterTypes(IContainer container);
    protected abstract void CreateWindow(IGraphicsService graphicsService);
    protected abstract void SetupAssetBundles(IAssetService assetService);
}
