#nullable enable
using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Resources
{
    internal class LazyResourceManager<T> : ResourceManager<T>
    {
        private readonly HashSet<string> _failedLoads;

        internal LazyResourceManager(string localDirectory, Func<string, T> resourceLoader, Func<string, bool>? resourceValidator = null)
            : base(localDirectory, resourceLoader, resourceValidator) {
            this._failedLoads = new HashSet<string>();
        }

        internal override T GetResource(string resourceKey) {
            resourceKey = resourceKey.ToLower();
            Debug.Assert(!this._failedLoads.Contains(resourceKey));
            if (!this._resources.ContainsKey(resourceKey)) {
                this.Load(Path.Join(this._fullResourceDirectory, resourceKey));
            }
            return base.GetResource(resourceKey);
        }
    }
}
