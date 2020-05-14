#nullable enable
using Annex.Assets;
using SFML.Audio;
using System.IO;
using static Annex.Audio.Sfml.Errors;

namespace Annex.Audio.Sfml
{
    public class SfmlAudioInitializer : IAssetInitializer
    {
        public string AssetPath { get; set; }

        public SfmlAudioInitializer(string path) {
            this.AssetPath = path;
        }

        public void CopyAssetToBinary(string path, IAssetLoader assetLoader) {

        }

        public object? Load(AssetInitializerArgs args, IAssetLoader assetLoader) {
            Debug.Assert(args is SfmlAudioInitializerArgs, INVALID_INITIALIZER_ARGS.Format(nameof(SfmlAudioInitializer), nameof(SfmlAudioInitializerArgs)));
            var sfmlArgs = (SfmlAudioInitializerArgs)args;

            switch (sfmlArgs.BufferMode) {
                case BufferMode.Buffered:
                    return new Music(assetLoader.GetString(args.Key));
                default:
                case BufferMode.None:
                    return new Sound(new SoundBuffer(assetLoader.GetString(args.Key)));
            }
        }

        public bool Validate(AssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".wav") || args.Key.EndsWith(".flac");
        }
    }
}
