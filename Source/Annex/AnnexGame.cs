using Annex.Events;
using Annex.Graphics;
using Annex.Scenes;
using Annex.Scenes.Components;

namespace Annex
{
    public static class AnnexGame
    {
        public static void Initialize() {
            var events = EventManager.Singleton;
            var window = GameWindow.Singleton;
            events.AddEvent(PriorityType.GRAPHICS, () => {
                window.Canvas.BeginDrawing();
                SceneManager.Singleton.CurrentScene.Draw(window.Canvas);
                window.Canvas.EndDrawing();
                return ControlEvent.NONE;
            }, 16, 0, GameWindow.DrawGameEventID);
            events.AddEvent(PriorityType.INPUT, () => {
                window.Canvas.ProcessEvents();
                return ControlEvent.NONE;
            }, 16, 0, GameWindow.DrawGameEventID);
        }

        public static void Start<T>() where T : Scene, new() {
            SceneManager.Singleton.LoadScene<T>();
            GameWindow.Singleton.Canvas.SetVisible(true);
            EventManager.Singleton.Run();
        }
    }
}
