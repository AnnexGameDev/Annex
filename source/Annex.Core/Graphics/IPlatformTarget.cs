namespace Annex.Core.Graphics;

public interface IPlatformTarget : IDisposable
{
    object Target { get; }
}
