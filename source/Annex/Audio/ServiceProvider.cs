using Annex.Assets.Loaders;
using Annex.Assets.Managers;
using Annex.Audio;
using Annex.Audio.Sfml;
using System.IO;
using static Annex.Strings.Paths;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static IAudioPlayer AudioManager => Locate<IAudioPlayer>();

        public class DefaultAudioManager : UncachedAssetManager {
        
            public DefaultAudioManager()
                : base(new FileLoader(), new SfmlAudioInitializer(Path.Combine(ApplicationPath, "audio/"))) {
            }
        }
    }
}
