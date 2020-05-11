#nullable enable

namespace Annex.Resources
{
    public abstract class ResourceManager : IResourceManager
    {
        public readonly IDataLoader DataLoader;
        public readonly IResourceLoader ResourceLoader;

        public ResourceManager(IDataLoader dataLoader, IResourceLoader resourceLoader) {
            this.DataLoader = dataLoader;
            this.ResourceLoader = resourceLoader;
        }

        public bool GetResource(IResourceLoaderArgs args, out object? resource) {
            if (!this.ResourceLoader.Validate(args)) {
                ServiceProvider.Log.WriteLineWarning($"The resource '{args}' is not valid");
                resource = default;
                return false;
            }

            if (!this.ContainsCachedResource(args.Key)) {
                var loadedResource = this.ResourceLoader.Load(args, this.DataLoader);
                Debug.Assert(loadedResource != null, $"Loaded resource {args.Key} is null");
#pragma warning disable CS8604 // Possible null reference argument.
                this.CacheResource(args.Key, loadedResource);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            resource = this.RetrieveCachedResource(args.Key);
            return true;
        }

        protected abstract object RetrieveCachedResource(string key);
        protected abstract void CacheResource(string key, object resource);
        protected abstract bool ContainsCachedResource(string key);
    }
}
