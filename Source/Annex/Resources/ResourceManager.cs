#nullable enable
using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Resources
{
    internal abstract class ResourceManager<T>
    {
        private protected readonly Dictionary<string, T> _resources;
        private readonly Func<string, T> _resourceLoader;
        private readonly Func<string, bool>? _resourceValidator;
        private protected readonly string _fullResourceDirectory;

        internal ResourceManager(string localDirectory, Func<string, T> resourceLoader, Func<string, bool>? resourceValidator = null) {
            this._resources = new Dictionary<string, T>();
            this._fullResourceDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources/", localDirectory);
            this._resourceLoader = resourceLoader;
            this._resourceValidator = resourceValidator;
        }

        private protected void Load(IEnumerable<string> files) {
            foreach (string file in files) {
                this.Load(file);
            }
        }

        private protected void Load(string file) {
            if (this._resourceValidator == null || this._resourceValidator.Invoke(file)) {
                string key = file.Remove(0, this._fullResourceDirectory.Length).ToLower().Replace('\\', '/');
                this._resources.Add(key, this._resourceLoader(file));
            }
        }

        internal virtual T GetResource(string resourceKey) {
            resourceKey = resourceKey.ToLower();
            Debug.Assert(this._resources.ContainsKey(resourceKey));
            return this._resources[resourceKey];
        }
    }
}
