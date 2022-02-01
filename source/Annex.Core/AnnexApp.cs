using Annex.Core.Events;
using Annex.Core.Services;
using Annex.Core.Times;

namespace Annex.Core;

public abstract class AnnexApp
{
    public AnnexApp() {
        var container = new Container();

        var asSingleton = new RegistrationOptions() { Singleton = true };
        container.Register<IEventScheduler, EventScheduler>();
        container.Register<ITimeService, StopwatchTimeService>(asSingleton);

        this.RegisterTypes(container);
    }

    protected abstract void RegisterTypes(IContainer container);
}
