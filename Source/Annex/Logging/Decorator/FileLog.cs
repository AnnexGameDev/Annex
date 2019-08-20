#nullable enable
using System;
using System.IO;

namespace Annex.Logging.Decorator
{
    public class FileLog : DecoratableLog
    {
        private const string LOG_FOLDER = "./logs/";
        private readonly string _logFile = Path.Combine(LOG_FOLDER, DateTime.Now.ToString("mm.dd.yyyy.hh.mm.ss tt") + ".txt");

        static FileLog() {
            Directory.CreateDirectory(LOG_FOLDER);
        }

        public FileLog(DecoratableLog? @base = null) : base(@base) {
        }

        private protected override void Append(string content) {
            File.AppendAllText(this._logFile, content);
        }
    }
}
