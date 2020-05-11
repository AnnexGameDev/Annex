#nullable enable
using System.Collections.Generic;

namespace Annex.Resources.Managers
{
    public class CachedResourceManager : ResourceManager
    {
        private readonly Dictionary<string, object> _resources;

        public CachedResourceManager(IDataLoader dataLoader, IResourceLoader resourceLoader) 
            : base(dataLoader, resourceLoader) {
            this._resources = new Dictionary<string, object>();
        }

        protected override void CacheResource(string key, object resource) {
            this._resources.Add(key, resource);
        }

        protected override bool ContainsCachedResource(string key) {
            return this._resources.ContainsKey(key);
        }

        protected override object RetrieveCachedResource(string key) {
            return this._resources[key];
        }
    }
}