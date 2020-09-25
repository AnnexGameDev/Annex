using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Managers;
using Annex.Audio;
using Annex.Audio.Sfml;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes;
using Annex.Scenes.Components;
using Annex.Services;

namespace Annex
{
    public static class AnnexGame
    {
        public static void Initialize() {
            var container = ServiceContainerSingleton.Create();
            container.Provide<Log>(new Log());
            ServiceProvider.Log.WriteLineTrace_Module("AnnexGame", "Initializing services...");
            container.Provide<EventService>();
            container.Provide<IAudioService>(new SfmlPlayer(new DefaultAudioManager()));
            container.Provide<ICanvas>(new SfmlCanvas(new DefaultTextureManager(), new DefaultFontManager(), new DefaultIconManager()));
            container.Provide<SceneService>();
        }

        public static void Start<T>() where T : Scene, new() {
            ServiceProvider.SceneService.LoadScene<T>();
            ServiceProvider.Canvas.SetVisible(true);
            ServiceProvider.EventService.Run();
        }

        private class DefaultAudioManager : CachedAssetManager
        {
            public DefaultAudioManager() : base(AssetType.Audio, new FileLoader(), new SfmlAudioInitializer("audio/")) {
            }
        }

        private class DefaultFontManager : CachedAssetManager
        {
            public DefaultFontManager() : base(AssetType.Font, new FileLoader(), new SfmlFontInitializer("fonts/")) {
            }
        }

        private class DefaultTextureManager : CachedAssetManager
        {
            public DefaultTextureManager() : base(AssetType.Texture, new FileLoader(), new SfmlTextureInitializer("textures/")) {
            }
        }

        private class DefaultIconManager : CachedAssetManager
        {
            public DefaultIconManager() : base(AssetType.Icon, new FileLoader(), new SfmlIconInitializer("icons/")) {
            }
        }
    }
}
