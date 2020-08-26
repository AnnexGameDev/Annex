namespace Annex.Logging
{
    public interface ILogable
    {
        void Write(string content);
        void WriteLine(string line);
    }
}
