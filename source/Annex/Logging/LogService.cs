using Annex.Assets;
using Annex.Logging.Decorator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Annex.Logging
{
    public class LogService : ILogService
    {
        private readonly object _lock;
        private readonly DecoratableLog _log;
        private readonly bool[] _allowedChannels;

        public LogService() {
            this._lock = new object();
            this._allowedChannels = new bool[Enum.GetNames(typeof(OutputChannel)).Length];
            this.EnableChannel(OutputChannel.Error);

            if (this.ConsoleExists()) {
                this._log = new FileLog(new ConsoleLog());
            } else {
                this._log = new FileLog();
            }
        }

        public void EnableChannel(OutputChannel channel) {
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
                this._log.WriteLine($"{GetCurrentTime()} - {line}");
            }
        }

        public void WriteLineVerbose(string line) {
            this.WriteLineChannel(line, OutputChannel.Verbose);
        }

        public void WriteLineWarning(string line) {
            this.WriteLineChannel(line, OutputChannel.Warning);
        }

        public void WriteLineError(string line) {
            this.WriteLineChannel(line, OutputChannel.Error);
        }

        public void WriteLineTrace(object sender, string line) {
            this.WriteLineTrace_Module(sender.GetType().Name, line);
        }

        public void WriteLineTrace_Module(string moduleName, string line) {
            this.WriteLineChannel($"{Process.GetCurrentProcess().Id}.{Thread.CurrentThread.ManagedThreadId} - [{moduleName}] - {line}", OutputChannel.Trace);
        }

        public void WriteLineChannel(string line, OutputChannel channel) {
            this.WriteLineClean($"[{channel}] - {line}");
        }

        private bool ConsoleExists() {
            try {
                Console.Title.Length.ToString();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        private string GetCurrentTime() {
            var now = DateTime.Now;
            return $"{ZeroPadNumber(now.Hour, 2)}:{ZeroPadNumber(now.Minute, 2)}:{ZeroPadNumber(now.Second, 2)}.{ZeroPadNumber(now.Millisecond, 3)}";
        }

        private string ZeroPadNumber(int val, int length) {
            string num = val.ToString();
            while (num.Length < length) {
                num = "0" + num;
            }
            return num;
        }
    }
}
