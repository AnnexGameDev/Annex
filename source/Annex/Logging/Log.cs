using Annex.Logging.Decorator;
using System;

namespace Annex.Logging
{
    public class Log : IService, ILogable
    {
        private readonly DecoratableLog _log;
        
        public Log() {
            if (this.ConsoleExists()) {
                this._log = new FileLog(new ConsoleLog());
            } else {
                this._log = new FileLog();
            }
        }

        public void Destroy() {

        }

        public void Write(string content) {
            this._log.Write(content);
        }

        public void WriteLine(string line) {
            this._log.WriteLine(line);
        }

        private bool ConsoleExists() {
            try {
                Console.Title.Length.ToString();
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}
