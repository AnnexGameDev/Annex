using Annex.Data;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.UserInterface.Components
{
    public class RenderBoxElement : UIElement
    {
        private protected readonly SurfaceContext RenderBox;
        public readonly PString RenderBoxSurface;

        public RenderBoxElement(string elementID = "") : base(elementID) {
            this.RenderBoxSurface = new PString();
            this.RenderBox = new SurfaceContext(this.RenderBoxSurface) {
                RenderPosition = this.Position,
                RenderSize = this.Size
            };
        }

        public override void Draw(IDrawableContext surfaceContext) {
            surfaceContext.Draw(this.RenderBox);
        }
    }
}
