#nullable enable
using System;
using System.IO;

namespace Annex.Resources
{
    internal class NonLazyResourceManager<T> : ResourceManager<T>
    {
        internal NonLazyResourceManager(string localDirectory, Func<string, T> resourceLoader, Func<string, bool>? resourceValidator = null) 
            : base(localDirectory, resourceLoader, resourceValidator) {
            this.Load(Directory.GetFiles(this._fullResourceDirectory, "*", SearchOption.AllDirectories));
        }
    }
}
