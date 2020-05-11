using Annex.Audio;
using Annex.Audio.Sfml;
using Annex.Resources.Loaders;
using Annex.Resources.Managers;
using System.IO;
using static Annex.Strings.Paths;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static IAudioPlayer AudioManager => Locate<IAudioPlayer>();

        public class DefaultAudioResourceManager : UncachedResourceManager {
        
            public DefaultAudioResourceManager()
                : base(new FileLoader(), new SfmlAudioLoader(Path.Combine(ApplicationPath, "audio/"))) {
            }
        }
    }
}
