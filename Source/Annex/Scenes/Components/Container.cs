using Annex.Graphics;
using System.Collections.Generic;

namespace Annex.Scenes.Components
{
    public class Container : Image
    {
        protected readonly List<UIElement> _children;

        public Container(string elementID = "") : base(elementID) {
            this._children = new List<UIElement>();
        }

        public override void Draw(IDrawableContext context) {
            if (!this.Visible) {
                return;
            }
            base.Draw(context);
            foreach (var child in this._children) {
                child.Draw(context);
            }
        }

        public void AddChild(UIElement child) {
            this._children.Add(child);
        }

        internal override bool HandleSceneFocusMouseDown(int x, int y) {
            // z-index from last to first.
            for (int i = this._children.Count - 1; i >= 0; i--) {
                if (this._children[i].HandleSceneFocusMouseDown(x, y)) {
                    return true;
                }
            }
            return false;
        }
    }
}
