using Annex.Events;
using Annex.Graphics;
using Annex.Logging;

namespace Annex
{
    public class Program
    {
        private static void Main() {
            Singleton.Create<Log>();
            var events = Singleton.Create<EventQueue>();
            var window = Singleton.Create<GameWindow>();

            events.AddEvent(PriorityType.GRAPHICS, () => {
                window.Context.BeginDrawing();

                window.Context.EndDrawing();
                return ControlEvent.NONE;
            }, 16, 0);

            window.Context.SetVisible(true);
            events.Run();
        }
    }
}
