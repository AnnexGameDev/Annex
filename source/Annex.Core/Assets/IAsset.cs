namespace Annex.Core.Assets
{
    public interface IAsset : IDisposable
    {
        string Id { get; }

        object? Target { get; }
        bool FilepathSupported { get; }
        string FilePath { get; }

        void SetTarget(object? target);
        byte[] ToBytes();
    }
}