#nullable enable
using System.Collections.Generic;

namespace Annex.Resources.Managers
{
    public class UncachedResourceManager : ResourceManager
    {
        private Dictionary<string, object> _temp_cache;

        public UncachedResourceManager(IDataLoader dataLoader, IResourceLoader resourceLoader) : base(dataLoader, resourceLoader) {
            this._temp_cache = new Dictionary<string, object>();
        }

        protected override void CacheResource(string key, object resource) {
            this._temp_cache[key] = resource;
        }

        protected override bool ContainsCachedResource(string key) {
            return false;
        }

        protected override object RetrieveCachedResource(string key) {
            var resource = this._temp_cache[key];
            this._temp_cache.Remove(key);
            return resource;
        }
    }
}
