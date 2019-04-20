using Annex.Logging;

namespace Annex
{
    public class Program
    {
        private static void Main() {
            Singleton.Create<Log>();
        }
    }
}
