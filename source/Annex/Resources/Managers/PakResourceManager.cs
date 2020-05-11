#nullable enable
using Annex.Resources.Loaders;
using System.Collections.Generic;

namespace Annex.Resources.Managers
{
    public class PakResourceManager : ResourceManager
    {
        private readonly Dictionary<string, object> _resources;

        public PakResourceManager(string pakFilePath, IResourceLoader resourceLoader)
            : this(new PakLoader(PakFile.CreateForInput(pakFilePath)), resourceLoader) {
        }

        public PakResourceManager(IDataLoader dataLoader, IResourceLoader resourceLoader)
            : base(dataLoader, resourceLoader) {
            this._resources = new Dictionary<string, object>();
        }

        protected override void CacheResource(string key, object resource) {
            this._resources[key] = resource;
        }

        protected override bool ContainsCachedResource(string key) {
            return this._resources.ContainsKey(key);
        }

        protected override object RetrieveCachedResource(string key) {
            return this._resources[key];
        }
    }
}
