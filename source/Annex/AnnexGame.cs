using Annex.Audio;
using Annex.Audio.Sfml;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes.Components;
using static Annex.Graphics.EventIDs;

namespace Annex
{
    public static class AnnexGame
    {
        public static void Initialize() {

            ServiceProvider.Provide<Log>(new Log());
            ServiceProvider.Log.WriteLineTrace_Module("AnnexGame", "Initializing services...");
            ServiceProvider.Provide<IAudioPlayer>(new SfmlPlayer(new ServiceProvider.DefaultAudioManager()));
            ServiceProvider.Provide<ICanvas>(new SfmlCanvas(new ServiceProvider.DefaultTextureManager(), new ServiceProvider.DefaultFontManager(), new ServiceProvider.DefaultIconManager()));

            ServiceProvider.EventManager.AddEvent(PriorityType.GRAPHICS, () => {
                var canvas = ServiceProvider.Canvas;
                canvas.BeginDrawing();
                ServiceProvider.SceneManager.CurrentScene.Draw(canvas);
                canvas.EndDrawing();
                return ControlEvent.NONE;
            }, 16, 0, DrawGameEventID);
            ServiceProvider.EventManager.AddEvent(PriorityType.INPUT, () => {
                var canvas = ServiceProvider.Canvas;
                canvas.ProcessEvents();
                return ControlEvent.NONE;
            }, 16, 0, DrawGameEventID);
        }

        public static void Start<T>() where T : Scene, new() {
            ServiceProvider.SceneManager.LoadScene<T>();
            ServiceProvider.Canvas.SetVisible(true);
            ServiceProvider.EventManager.Run();
        }
    }
}
