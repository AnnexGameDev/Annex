using Annex.Assets;
using SFML.Audio;

namespace Annex.Audio.Sfml.Assets
{
    public class UnbufferedAudioAsset : Asset
    {
        public UnbufferedAudioAsset(string id, byte[] targetData) : base(id, new Sound(new SoundBuffer(targetData))) {
        }

        public override void Dispose() {
            var sound = this.GetTarget() as Sound;
            sound?.Stop();
            sound?.Dispose();
        }
    }
}
