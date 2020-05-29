using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Managers;
using Annex.Audio;
using Annex.Audio.Sfml;
using System.IO;
using static Annex.Paths;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static IAudioService AudioService => Locate<IAudioService>();

        public class DefaultAudioManager : UncachedAssetManager {
        
            public DefaultAudioManager()
                : base(AssetType.Audio, new FileLoader(), new SfmlAudioInitializer(Path.Combine(ApplicationPath, "audio/"))) {
            }
        }
    }
}
