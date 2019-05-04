
using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Resources
{
    public class ResourceManager<T>
    {
        private readonly Dictionary<string, T> _resources;
        private readonly Func<string, T> _resourceLoader;
        private readonly Func<string, bool>? _resourceValidator;
        private readonly string _fullResourceDirectory;

        /// <summary>
        /// </summary>
        /// <param name="localDirectory">The local path to the resource folder.</param>
        /// <param name="resourceLoader">Loads the resource given the path. Returns whether or not it was successful.</param>
        /// <param name="lazyLoading">Only load resources as they're needed.</param>
        /// <param name="resourceValidator">Validates whether or not a path in the directory is a valid resource file.</param>
        public ResourceManager(string localDirectory, Func<string, T> resourceLoader, bool lazyLoading = true, Func<string, bool>? resourceValidator = null) {
            this._resources = new Dictionary<string, T>();
            this._fullResourceDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localDirectory);
            this._resourceLoader = resourceLoader;
            this._resourceValidator = resourceValidator;

            if (!lazyLoading) {
                this.Load(Directory.GetFiles(this._fullResourceDirectory, "*", SearchOption.AllDirectories));
            }
        }

        private void Load(IEnumerable<string> files) {
            foreach (string file in files) {
                if (this._resourceValidator == null || this._resourceValidator.Invoke(file)) {
                    string key = file.Remove(0, this._fullResourceDirectory.Length).ToLower().Replace('\\', '/');
                    this._resources.Add(key, this._resourceLoader(file));
                }
            }
        }

        public T GetResource(string resourceKey) {
            resourceKey = resourceKey.ToLower();
            Debug.Assert(this._resources.ContainsKey(resourceKey));
            return this._resources[resourceKey];
        }
    }
}
