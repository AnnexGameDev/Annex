using Annex;
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
using Annex.Services;
using AnnexSinglePlayer.Scenes.MainMenu;
using System.IO;
using static Annex.Paths;

namespace AnnexSinglePlayer
{
    public class Game
    {
        private static void Main(string[] args) {
            var container = ServiceContainerSingleton.Create();
            container.Provide<Log>();
            ServiceProvider.Log.WriteLineTrace_Module("AnnexGame", "Initializing services...");
            container.Provide<EventService>();
            container.Provide<IAudioService>(new SfmlPlayer(new DefaultAudioManager()));
            container.Provide<ICanvas>(new SfmlCanvas(new DefaultTextureManager(), new DefaultFontManager(), new DefaultIconManager()));
            container.Provide<SceneService>();

            Debug.PackageAssetsToBinaryFrom(AssetType.Audio, Path.Combine(SolutionFolder, "assets/audio/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Texture, Path.Combine(SolutionFolder, "assets/textures/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Font, Path.Combine(SolutionFolder, "assets/fonts/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Icon, Path.Combine(SolutionFolder, "assets/icons/"));

            ServiceProvider.SceneService.LoadScene<MainMenuScene>();
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
