#nullable enable
using Annex.Resources;
using SFML.Audio;
using System.IO;

namespace Annex.Audio.Sfml
{
    public class SfmlAudioLoader : IResourceLoader
    {
        public readonly string ResourcePath;

        public SfmlAudioLoader(string path) {
            this.ResourcePath = path;
        }

        public object? Load(IResourceLoaderArgs args, IDataLoader resourceLoader) {
            if (!(args is SfmlAudioLoaderArgs sfmlArgs)) {
                Debug.Error($"{nameof(SfmlAudioLoader)} requires {nameof(SfmlAudioLoaderArgs)} args");
                return null;
            }

            switch (sfmlArgs.BufferMode) {
                case BufferMode.Buffered:
                    return new Music(resourceLoader.GetString(args.Key));
                default:
                case BufferMode.None:
                    return new Sound(new SoundBuffer(resourceLoader.GetString(args.Key)));
            }
        }

        public bool Validate(IResourceLoaderArgs args) {
            args.Key = Path.Combine(this.ResourcePath, args.Key);
            return args.Key.EndsWith(".wav") || args.Key.EndsWith(".flac");
        }
    }
}
