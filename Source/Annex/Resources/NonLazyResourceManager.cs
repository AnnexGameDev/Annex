using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Resources
{
    public class NonLazyResourceManager<T> : ResourceManager<T>
    {
        public NonLazyResourceManager(string localDirectory, Func<string, T> resourceLoader, Func<string, bool>? resourceValidator = null) 
            : base(localDirectory, resourceLoader, resourceValidator) {
            this.Load(Directory.GetFiles(this._fullResourceDirectory, "*", SearchOption.AllDirectories));
        }
    }
}
