namespace Annex.Core.Graphics.Contexts;

public abstract class DrawContext : IDisposable
{
    public IPlatformTarget? PlatformTarget { get; private set; }

    public string? Camera { get; init; } = KnownCamera.Default;
    public Shader? Shader { get; set; } = null;

    public void SetPlatformTarget(IPlatformTarget? platformTarget)
    {
        PlatformTarget?.Dispose();
        PlatformTarget = platformTarget;
    }

    public void Dispose()
    {
        SetPlatformTarget(null);
    }
}