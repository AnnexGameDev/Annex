using System.Text;

namespace Annex.Core.Logging
{
    public class LogEntry
    {
        public DateTime DateTime { get; set; }
        public LogSeverity Severity { get; set; }
        public string Message { get; set; } = string.Empty;
        public Exception? AssociatedException { get; set; } = null;

        public int SourceLine { get; set; } = -1;
        public string SourceFile { get; set; } = string.Empty;
        public string CallerMemberName { get; set; } = string.Empty;
        public int ThreadId { get; set; } = 0;

        public LogEntry() {
            this.DateTime = DateTime.Now;
            this.ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public override string ToString() {

            string exceptionDetails = this.GetExceptionDetails();

            return $"{this.DateTime.ToLocalTime()} - [{this.Severity}] - {this.Message}{Environment.NewLine}" +
                $"TID: {this.ThreadId}{Environment.NewLine}" +
                $"{this.SourceFile}:{this.CallerMemberName}:{this.SourceLine}{Environment.NewLine}" +
                $"{exceptionDetails}{Environment.NewLine}";

        }

        private string GetExceptionDetails() {
            if (this.AssociatedException == null) {
                return string.Empty;
            }

            var sb = new StringBuilder();
            var ex = this.AssociatedException;
            string tabs = "";

            while (ex != null) {
                sb.AppendLine($"{tabs}{ex.Message}");
                sb.AppendLine($"{tabs}{ex.StackTrace}");

                ex = ex.InnerException;
                tabs += "----";
            }

            return sb.ToString();
        }
    }
}