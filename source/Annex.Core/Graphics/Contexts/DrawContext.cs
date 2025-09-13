namespace Annex.Core.Graphics.Contexts;

public abstract class DrawContext : IDisposable
{
    public IDisposable? PlatformTarget { get; private set; }

    public string? Camera { get; init; } = CameraId.Default.ToString();
    public Shader? Shader { get; init; } = null;

    public void SetPlatformTarget(IDisposable? platformTarget)
    {
        PlatformTarget?.Dispose();
        PlatformTarget = platformTarget;
    }

    public void Dispose()
    {
        SetPlatformTarget(null);
    }
}