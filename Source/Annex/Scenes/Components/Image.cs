using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.Scenes.Components
{
    public class Image : UIElement
    {
        protected readonly SurfaceContext ImageContext;
        public readonly String ImageSurface;

        public Image(string elementID = "") : base(elementID) {
            this.ImageSurface = new String();
            this.ImageContext = new SurfaceContext(this.ImageSurface) {
                RenderPosition = this.Position,
                RenderSize = this.Size,
                UseUIView = true
            };
        }

        public override void Draw(IDrawableContext surfaceContext) {
            surfaceContext.Draw(this.ImageContext);
        }
    }
}
