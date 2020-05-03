using Annex.Events;
using Annex.Graphics;
using Annex.Scenes.Components;

namespace Annex
{
    public static class AnnexGame
    {
        public static void Initialize() {
            ServiceProvider.Log.WriteLineTrace_Module("AnnexGame", "Initializing...");
            var events = ServiceProvider.EventManager;
            var canvas = ServiceProvider.Canvas;
            events.AddEvent(PriorityType.GRAPHICS, () => {
                canvas.BeginDrawing();
                ServiceProvider.SceneManager.CurrentScene.Draw(canvas);
                canvas.EndDrawing();
                return ControlEvent.NONE;
            }, 16, 0, Canvas.DrawGameEventID);
            events.AddEvent(PriorityType.INPUT, () => {
                canvas.ProcessEvents();
                return ControlEvent.NONE;
            }, 16, 0, Canvas.DrawGameEventID);
        }

        public static void Start<T>() where T : Scene, new() {
            ServiceProvider.SceneManager.LoadScene<T>();
            ServiceProvider.Canvas.SetVisible(true);
            ServiceProvider.EventManager.Run();
        }
    }
}
