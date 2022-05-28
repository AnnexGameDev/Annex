using Annex_Old.Data.Shared;
using Annex_Old.Graphics;
using Annex_Old.Graphics.Contexts;

namespace Annex_Old.Scenes.Components
{
    public class Image : UIElement
    {
        protected readonly TextureContext ImageContext;
        public readonly String ImageTextureName;

        public Image(string elementID = "") : base(elementID) {
            this.ImageTextureName = new String();
            this.ImageContext = new TextureContext(this.ImageTextureName) {
                RenderPosition = this.Position,
                RenderSize = this.Size,
                UseUIView = true
            };
        }

        public override void Draw(ICanvas canvas) {
            if (!this.Visible) {
                return;
            }
            canvas.Draw(this.ImageContext);
        }
    }
}
