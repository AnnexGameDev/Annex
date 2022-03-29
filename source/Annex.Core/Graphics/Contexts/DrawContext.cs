namespace Annex.Core.Graphics.Contexts
{
    public abstract class DrawContext : IDisposable
    {
        public IDisposable? PlatformTarget { get; private set; }

        public string? Camera { get; init; }

        public void SetPlatformTarget(IDisposable? platformTarget) {
            this.PlatformTarget?.Dispose();
            this.PlatformTarget = platformTarget;
        }

        public void Dispose() {
            this.SetPlatformTarget(null);
        }
    }
}