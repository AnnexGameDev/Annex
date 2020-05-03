#nullable enable
using System;
using System.Collections.Generic;

namespace Annex.Resources
{
    public class ResourceManagerRegistry : IService
    {
        private readonly Dictionary<ResourceType, ResourceManager> _resourceManagers;

        public ResourceManagerRegistry() {
            this._resourceManagers = new Dictionary<ResourceType, ResourceManager>();
        }

        private void Register<T>(ResourceType resourceType) where T : ResourceManager, new() {
            Debug.Assert(!this._resourceManagers.ContainsKey(resourceType), $"The resource manager for {resourceType} already exists");
            this._resourceManagers[resourceType] = new T();
        }

        public ResourceManager? GetResourceManager(ResourceType resourceType) {
            if (this.Exists(resourceType)) {
                return this._resourceManagers[resourceType];
            }
            return null;
        }

        public ResourceManager GetOrCreate<T>(ResourceType resourceType) where T : ResourceManager, new() {
            // Is it the first register?
            if (!this.Exists(resourceType)) {
                this.Register<T>(resourceType);
            }
            return this._resourceManagers[resourceType];
        }

        public bool Exists(ResourceType resourceType) {
            return this._resourceManagers.ContainsKey(resourceType);
        }

        public void Destroy() {

        }
    }
}
