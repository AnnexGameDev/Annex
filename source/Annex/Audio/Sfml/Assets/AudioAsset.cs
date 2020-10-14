using Annex.Assets;

namespace Annex.Audio.Sfml.Assets
{
    public class AudioAsset : Asset
    {
        public AudioAsset(string id, byte[] targetData) : base(id, targetData) {
        }

        public override void Dispose() {
        }
    }
}
