#nullable enable
using System.Collections.Generic;

namespace Annex.Resources
{
    public abstract class ResourceManager
    {
        public delegate object ResourceLoader_Bytes(byte[] data);
        public delegate object ResourceLoader_String(string id);
        public delegate bool ResourceValidator(string id);

        protected string? _resourcePath;
        protected ResourceLoader_Bytes? _resourceLoader_FromBytes;
        protected ResourceLoader_String? _resourceLoader_FromString;
        protected ResourceValidator? _resourceValidator;

        protected virtual void Load(IEnumerable<string> keys) {
            foreach (var key in keys) {
                this.Load(key);
            }
        }
        protected abstract void Load(string key);
        public abstract object GetResource(string key);

        public void SetResourcePath(string resourcePath) {
            Debug.Assert(this._resourcePath == null);
            this._resourcePath = resourcePath;
        }

        public void SetResourceLoader(ResourceLoader_String loader) {
            Debug.Assert(this._resourceLoader_FromString == null);
            this._resourceLoader_FromString = loader;
        }

        public void SetResourceLoader(ResourceLoader_Bytes loader) {
            Debug.Assert(this._resourceLoader_FromBytes == null);
            this._resourceLoader_FromBytes = loader;
        }

        public void SetResourceValidator(ResourceValidator validator) {
            Debug.Assert(this._resourceValidator == null);
            this._resourceValidator = validator;
        }

        protected internal abstract void PackageResourcesToBinary(string baseDir);
    }
}
