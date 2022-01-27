using Annex.Core.Events;
using Annex.Core.Services;

namespace Annex.Core;

public abstract class AnnexApp
{
    public AnnexApp() {
        var container = new Container();

        container.Register<IEventScheduler, EventScheduler>();

        this.RegisterTypes(container);
    }

    protected abstract void RegisterTypes(IContainer container);
}
