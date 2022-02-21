namespace Annex.Core.Assets
{
    // TODO: Tests?
    internal abstract class Asset : IAsset
    {
        public IDisposable? Target { get; private set; }

        public abstract bool FilepathSupported { get; }
        public abstract string FilePath { get; }
        public abstract byte[] ToBytes();

        public void Dispose() {
            this.SetTarget(null);
        }

        public void SetTarget(IDisposable? target) {
            this.Target?.Dispose();
            this.Target = target;
        }
    }
}