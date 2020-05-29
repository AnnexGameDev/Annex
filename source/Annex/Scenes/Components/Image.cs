using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.Scenes.Components
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
