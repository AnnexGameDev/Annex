using Annex_Old.Assets;
using SFML.Graphics;

namespace Annex_Old.Graphics.Sfml.Assets
{
    public class IconAsset : Asset
    {
        public IconAsset(string id, byte[] targetData) : base(id, new Image(targetData)) {
        }

        public override void Dispose() {
            var icon = this.GetTarget() as Image;
            icon?.Dispose();
        }
    }
}