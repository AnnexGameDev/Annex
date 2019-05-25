using Annex.Graphics;

namespace Annex.UserInterface.Components
{
    public abstract class UIElement : IDrawableObject
    {
        private readonly string ElementID;
        public (float width, float height) Size;
        public (float left, float top) Position;

        public UIElement(string elementID) {
            this.ElementID = elementID;
            this.Size = (0, 0);
            this.Position = (0, 0);
        }

        public abstract void Draw(IDrawableContext surfaceContext);

        public virtual UIElement? GetElementById(string id) {
            if (this.ElementID == id) {
                return this;
            }
            return null;
        }
    }
}
