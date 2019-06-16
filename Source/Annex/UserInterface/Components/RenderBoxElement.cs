using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.UserInterface.Components
{
    public abstract class RenderBoxElement : UIElement
    {
        protected readonly SurfaceContext RenderBox;
        public readonly String RenderBoxSurface;

        public RenderBoxElement(string elementID = "") : base(elementID) {
            this.RenderBoxSurface = new String();
            this.RenderBox = new SurfaceContext(this.RenderBoxSurface) {
                RenderPosition = this.Position,
                RenderSize = this.Size,
                UseUIView = true
            };
        }

        public override void Draw(IDrawableContext surfaceContext) {
            surfaceContext.Draw(this.RenderBox);
        }
    }
}
