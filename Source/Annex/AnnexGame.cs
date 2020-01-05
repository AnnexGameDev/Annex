using Annex.Events;
using Annex.Graphics;
using Annex.Scenes;
using Annex.Scenes.Components;

namespace Annex
{
    public static class AnnexGame
    {
        public static void Initialize() {
            var events = ServiceProvider.EventManager;
            var canvas = ServiceProvider.Canvas;
            var audio = ServiceProvider.AudioManager;
            events.AddEvent(PriorityType.GRAPHICS, () => {
                canvas.BeginDrawing();
                ServiceProvider.SceneManager.CurrentScene.Draw(canvas);
                canvas.EndDrawing();
                return ControlEvent.NONE;
            }, 16, 0, ICanvas.DrawGameEventID);
            events.AddEvent(PriorityType.INPUT, () => {
                canvas.ProcessEvents();
                return ControlEvent.NONE;
            }, 16, 0, ICanvas.DrawGameEventID);
        }

        public static void Start<T>() where T : Scene, new() {
            ServiceProvider.SceneManager.LoadScene<T>();
            ServiceProvider.Canvas.SetVisible(true);
            ServiceProvider.EventManager.Run();
        }
    }
}
