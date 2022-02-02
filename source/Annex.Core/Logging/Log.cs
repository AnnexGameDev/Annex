using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text;

namespace Annex.Core.Logging
{
    public class Log : ILogService, IDisposable
    {
        private const int FlushQueueLimit = 100;
        private static ConcurrentQueue<LogEntry> _unflushedEntries = new();
        private static readonly string _logDirectory;
        private readonly string _logFilePath;

        private static ILogService? _singletonInstance;

        static Log() {
            _logDirectory = Path.Combine(Paths.ApplicationPath, "logs/");
            Directory.CreateDirectory(_logDirectory);
        }

        public Log() {
            _logFilePath = Path.Combine(_logDirectory, DateTime.Now.ToFileTimeUtc() + ".txt");
            _singletonInstance = this;
            this.Flush().FireAndForget();
        }

        public void Trace(LogEntry logEntry) {
            _unflushedEntries.Enqueue(logEntry);

            if (_unflushedEntries.Count > FlushQueueLimit) {
                this.Flush().FireAndForget();
            }
        }

        private Task Flush() {
            var sb = new StringBuilder();
            while (_unflushedEntries.TryDequeue(out var result)) {
                sb.Append(result.ToString());
            }
            return File.AppendAllTextAsync(this._logFilePath, sb.ToString());
        }

        public void Dispose() {
            this.Flush().FireAndForget();
            _singletonInstance = null;
        }

        public static void Trace(LogSeverity severity, string message, Exception? exception = null,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = ""
        ) {
            var entry = new LogEntry() {
                Severity = severity,
                Message = message,
                AssociatedException = exception,
                CallerMemberName = memberName,
                SourceLine = lineNumber,
                SourceFile = filePath
            };

            if (_singletonInstance == null) {
                _unflushedEntries.Enqueue(entry);
            } else {
                _singletonInstance.Trace(entry);
            }
        }
    }
}