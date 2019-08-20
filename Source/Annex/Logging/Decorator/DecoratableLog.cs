#nullable enable
using System;

namespace Annex.Logging.Decorator
{
    public abstract class DecoratableLog : ILogable
    {
        private readonly DecoratableLog? _base;

        public DecoratableLog(DecoratableLog? @base = null) {
            this._base = @base;
        }

        private protected abstract void Append(string content);

        public void Write(string content) {
            this.Append(content);
            this._base?.Write(content);
        }

        public void WriteLine(string line) {
            this.Write(line + Environment.NewLine);
        }
    }
}
