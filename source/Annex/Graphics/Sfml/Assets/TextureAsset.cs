using Annex.Assets;
using SFML.Graphics;

namespace Annex.Graphics.Sfml.Assets
{
    public class TextureAsset : Asset
    {
        public TextureAsset(string id, byte[] targetData) : base(id, new Texture(targetData)) {
        }

        public override void Dispose() {
            var texture = this.GetTarget() as Texture;
            texture?.Dispose();
        }
    }
}
