using Annex.Graphics;
using System.Collections.Generic;

namespace Annex.UserInterface.Components
{
    public class Container : RenderBoxElement
    {
        private protected readonly List<UIElement> _children;

        public Container(string elementID = "") : base(elementID) {
            this._children = new List<UIElement>();
        }

        public override void Draw(IDrawableContext surfaceContext) {
            base.Draw(surfaceContext);
            foreach (var child in this._children) {
                child.Draw(surfaceContext);
            }
        }

        public void AddChild(UIElement child) {
            this._children.Add(child);
        }
    }
}
