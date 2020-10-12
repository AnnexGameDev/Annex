using Annex.Assets;
using SFML.Audio;

namespace Annex.Audio.Sfml.Assets
{
    public class BufferedAudioAsset : Asset
    {
        public BufferedAudioAsset(string id, byte[] targetData) : base(id, new Music(targetData)) {
        }

        public override void Dispose() {
            var music = this.GetTarget() as Music;
            music?.Stop();
            music?.Dispose();
        }
    }
}