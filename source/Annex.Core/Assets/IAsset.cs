namespace Annex.Core.Assets
{
    public interface IAsset : IDisposable
    {
        object Target { get; }
    }
}