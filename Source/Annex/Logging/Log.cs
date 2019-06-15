using Annex.Logging.Decorator;
using System;

namespace Annex.Logging
{
    public class Log : Singleton, ILogable
    {
        private readonly DecoratableLog _log;

        public static Log Singleton => Get<Log>();
        static Log() {
            Create<Log>();
        }

        public Log() {
            if (this.ConsoleExists()) {
                this._log = new FileLog(new ConsoleLog());
            } else {
                this._log = new FileLog();
            }
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
