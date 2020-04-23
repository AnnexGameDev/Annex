using Annex.Audio;
using Annex.Audio.Sfml;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static IAudioPlayer AudioManager => Locate<IAudioPlayer>() ?? Provide<IAudioPlayer>(new SfmlPlayer());
    }
}
