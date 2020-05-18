using Annex.Audio;
using Annex.Audio.Sfml;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes.Components;

namespace Annex
{
    public static class AnnexGame
    {
        public static void Initialize() {
            ServiceProvider.Provide<Log>(new Log());
            ServiceProvider.Log.WriteLineTrace_Module("AnnexGame", "Initializing services...");
            ServiceProvider.Provide<IAudioService>(new SfmlPlayer(new ServiceProvider.DefaultAudioManager()));
            ServiceProvider.Provide<ICanvas>(new SfmlCanvas(new ServiceProvider.DefaultTextureManager(), new ServiceProvider.DefaultFontManager(), new ServiceProvider.DefaultIconManager()));
        }

        public static void Start<T>() where T : Scene, new() {
            ServiceProvider.SceneService.LoadScene<T>();
            ServiceProvider.Canvas.SetVisible(true);
            ServiceProvider.EventService.Run();
        }
    }
}
