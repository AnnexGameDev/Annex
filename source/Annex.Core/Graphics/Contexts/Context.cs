namespace Annex.Core.Graphics.Contexts
{
    public abstract class Context : IDisposable
    {
        public IDisposable? PlatformTarget { get; private set; }

        public void SetPlatformTarget(IDisposable? platformTarget) {
            this.PlatformTarget?.Dispose();
            this.PlatformTarget = platformTarget;
        }

        public void Dispose() {
            this.SetPlatformTarget(null);
        }
    }
}