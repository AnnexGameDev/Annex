using Annex_Old.Services;

namespace Annex_Old.Logging
{
    public interface ILogService : IService
    {
        void WriteLineWarning(string line);
        void WriteLineError(string line);

        void WriteLine(string line);
        void WriteLineTrace(object sender, string line);
        void WriteLineTrace_Module(string moduleName, string line);
    }
}
