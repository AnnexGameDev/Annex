using Annex.Assets;
using SFML.Graphics;

namespace Annex.Graphics.Sfml.Assets
{
    public class FontAsset : Asset
    {
        public FontAsset(string id, byte[] targetData) : base(id, new Font(targetData)) {
        }

        public override void Dispose() {
            var font = this.GetTarget() as Font;
            font?.Dispose();
        }
    }
}
