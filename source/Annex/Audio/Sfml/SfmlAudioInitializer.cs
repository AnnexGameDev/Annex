﻿#nullable enable
using Annex.Assets;
using SFML.Audio;
using System.IO;
using static Annex.Audio.Sfml.Errors;

namespace Annex.Audio.Sfml
{
    public class SfmlAudioInitializer : IAssetInitializer
    {
        public readonly string AssetPath;

        public SfmlAudioInitializer(string path) {
            this.AssetPath = path;
        }

        public object? Load(IAssetInitializerArgs args, IAssetLoader assetLoader) {
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

        public bool Validate(IAssetInitializerArgs args) {
            args.Key = Path.Combine(this.AssetPath, args.Key);
            return args.Key.EndsWith(".wav") || args.Key.EndsWith(".flac");
        }
    }
}
