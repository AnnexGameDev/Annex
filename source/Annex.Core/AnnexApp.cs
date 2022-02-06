using Annex.Core.Assets;
using Annex.Core.Events;
using Annex.Core.Graphics;
using Annex.Core.Logging;
using Annex.Core.Scenes;
using Annex.Core.Services;
using Annex.Core.Time;

namespace Annex.Core;

public abstract class AnnexApp
{
    private readonly IEventScheduler _eventService;
    private readonly IGraphicsService _graphicsService;
    private readonly IAssetService _assetManager;
    private readonly IContainer _container;

    public AnnexApp() {
        this._container = new Container();

        var asSingleton = new RegistrationOptions() { Singleton = true };
        this._container.Register<ILogService, Log>(asSingleton);
        this._container.Register<IEventScheduler, EventScheduler>(asSingleton);
        this._container.Register<ITimeService, StopwatchTimeService>(asSingleton);
        this._container.Register<ISceneService, SceneService>(asSingleton);
        this._container.Register<IGraphicsService, GraphicsService>(asSingleton);
        this._container.Register<IAssetService, AssetService>(asSingleton);

        this.RegisterTypes(this._container);

        this._eventService = this._container.Resolve<IEventScheduler>();
        this._graphicsService = this._container.Resolve<IGraphicsService>();
        this._assetManager = this._container.Resolve<IAssetService>();
    }


    public void Run() {
        try {
            this.SetupAssetBundles(this._assetManager);
            this.CreateWindow(this._graphicsService);
            this._eventService.Run();
            this._container.Dispose();
        }
        catch (Exception ex) {
            Log.Trace(LogSeverity.Error, "Exception in main gameloop", ex);
        }
    }

    protected abstract void RegisterTypes(IContainer container);
    protected abstract void CreateWindow(IGraphicsService graphicsService);
    protected abstract void SetupAssetBundles(IAssetService assetService);
}
