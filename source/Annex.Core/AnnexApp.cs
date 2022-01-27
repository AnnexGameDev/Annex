using Annex.Core.Services;

namespace Annex.Core;

public abstract class AnnexApp
{
    public AnnexApp() {
        var container = new Container();
        this.RegisterTypes(container);
    }

    protected abstract void RegisterTypes(IContainer container);
}
