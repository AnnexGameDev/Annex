using Annex.Core.Events;
using Annex.Core.Logging;
using Annex.Core.Scenes;
using Annex.Core.Services;
using Annex.Core.Time;

namespace Annex.Core;

public abstract class AnnexApp
{
    private readonly IEventScheduler _eventService;
    private readonly ISceneService _sceneService;
    private readonly IContainer _container;

    public AnnexApp() {
        this._container = new Container();

        var asSingleton = new RegistrationOptions() { Singleton = true };
        this._container.Register<ILogService, Log>(asSingleton);
        this._container.Register<IEventScheduler, EventScheduler>();
        this._container.Register<ITimeService, StopwatchTimeService>(asSingleton);
        this._container.Register<ISceneService, SceneService>(asSingleton);

        this.RegisterTypes(this._container);

        this._eventService = this._container.Resolve<IEventScheduler>();
        this._sceneService = this._container.Resolve<ISceneService>();
    }

    protected abstract void RegisterTypes(IContainer container);

    protected void Run<T>() where T : IScene {
        try {
            this._sceneService.LoadScene<T>();
            this._eventService.Run();
            this._container.Dispose();
        }
        catch (Exception ex) {
            Log.Trace(LogSeverity.Error, "Exception in main gameloop", ex);
        }
    }
}
