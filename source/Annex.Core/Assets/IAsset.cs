namespace Annex_Old.Core.Assets
{
    public interface IAsset : IDisposable
    {
        IDisposable? Target { get; }
        bool FilepathSupported { get; }
        string FilePath { get; }

        void SetTarget(IDisposable? target);
        byte[] ToBytes();
    }
}