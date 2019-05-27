using Annex.Data.Binding;
using Annex.Graphics;
using Annex.UserInterface.Controllers;

namespace Annex.UserInterface.Components
{
    public abstract class UIElement : InputController, IDrawableObject
    {
        private readonly string ElementID;
        public readonly PVector Size;
        public readonly PVector Position;

        public UIElement(string elementID) {
            this.ElementID = elementID;
            this.Size = new PVector(100, 100);
            this.Position = new PVector();
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
