using System;

namespace Annex.Logging.Decorator
{
    public abstract class DecoratableLog : ILogable
    {
        private DecoratableLog? _base;

        public DecoratableLog(DecoratableLog? @base = null) {
            _base = @base;
        }

        private protected abstract void Append(string content);

        public void Write(string content) {
            Append(content);
            _base?.Write(content);
        }

        public void WriteLine(string line) {
            Write(line + Environment.NewLine);
        }
    }
}
