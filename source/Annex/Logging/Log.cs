using Annex.Logging.Decorator;
using System;

namespace Annex.Logging
{
    public class Log : IService
    {
        private readonly object _lock;
        private readonly DecoratableLog _log;
        private readonly bool[] _allowedChannels;

        public Log() {
            this._lock = new object();
            this._allowedChannels = new bool[Enum.GetNames(typeof(OutputChannel)).Length];
            this.EnableChannel(OutputChannel.Error);

            if (this.ConsoleExists()) {
                this._log = new FileLog(new ConsoleLog());
            } else {
                this._log = new FileLog();
            }
        }

        private void EnableChannel(OutputChannel channel) {
            this._allowedChannels[(int)channel] = true;
        }

        public void Destroy() {

        }

        public void WriteClean(string content) {
            lock (this._lock) {
                this._log.Write(content);
            }
        }

        public void WriteLineClean(string line) {
            lock (this._lock) {
                this._log.WriteLine(line);
            }
        }

        public void WriteLineverbose(string message) {
            this.WriteLineChannel(message, OutputChannel.Verbose);
        }

        public void WriteLineWarning(string message) {
            this.WriteLineChannel(message, OutputChannel.Warning);
        }

        public void WriteLineError(string message) {
            this.WriteLineChannel(message, OutputChannel.Error);
        }

        public void WriteLineChannel(string message, OutputChannel channel) {
            this.WriteLineClean($"[{channel}] - {message}");
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
