using Annex.Events;
using Annex.Logging;

namespace Annex
{
    public class Program
    {
        private static void Main() {
            Singleton.Create<Log>();
            var events = Singleton.Create<EventQueue>();

            events.Run();
        }
    }
}
