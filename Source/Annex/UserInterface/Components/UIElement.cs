using Annex.Data;
using Annex.Graphics;
using Annex.UserInterface.Controllers;

namespace Annex.UserInterface.Components
{
    public abstract class UIElement : InputController, IDrawableObject
    {
        private readonly string ElementID;
        public readonly Vector2f Size;
        public readonly Vector2f Position;

        public UIElement(string elementID) {
            this.ElementID = elementID;
            this.Size = new Vector2f(100, 100);
            this.Position = new Vector2f();
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
