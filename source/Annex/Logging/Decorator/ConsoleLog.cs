#nullable enable
using System;

namespace Annex.Logging.Decorator
{
    public class ConsoleLog : DecoratableLog
    {
        public ConsoleLog(DecoratableLog? @base = null) : base(@base) {

        }

        private protected override void Append(string content) {
            Console.Write(content);
        }
    }
}
