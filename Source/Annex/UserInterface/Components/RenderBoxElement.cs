using Annex.Data.Binding;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.UserInterface.Components
{
    public abstract class RenderBoxElement : UIElement
    {
        protected readonly SurfaceContext RenderBox;
        public readonly PString RenderBoxSurface;

        public RenderBoxElement(string elementID = "") : base(elementID) {
            this.RenderBoxSurface = new PString();
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
