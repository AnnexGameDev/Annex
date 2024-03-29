﻿namespace Annex.Core.Assets
{
    // TODO: Tests?
    internal abstract class Asset : IAsset
    {
        public object? Target { get; private set; }

        public abstract bool FilepathSupported { get; }
        public abstract string FilePath { get; }
        public abstract byte[] ToBytes();

        public void Dispose() {
            this.SetTarget(null);
        }

        public void SetTarget(object? target) {
            if (this.Target is IDisposable disposable) {
                disposable.Dispose();
            }
            this.Target = target;
        }
    }
}