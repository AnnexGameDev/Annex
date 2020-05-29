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

        public override void Draw(ICanvas canvas) {
            if (!this.Visible) {
                return;
            }
            base.Draw(canvas);
            foreach (var child in this._children) {
                child.Draw(canvas);
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

        public override UIElement GetElementById(string id) {

            if (this.ElementID == id) {
                return this;
            }

            foreach (var child in this._children) {
                var c = child.GetElementById(id);

                if (c != null) {
                    return c;
                }
            }

            return null;
        }

        public override RemoveState RemoveElementById(string id) {

            if (this.ElementID == id) {
                return RemoveState.ShouldBeRemoved;
            }

            for (int i = 0; i < this._children.Count; i++) {
                switch (this._children[i].RemoveElementById(id)) {
                    case RemoveState.KeepSearching:
                        continue;
                    case RemoveState.WasRemoved:
                        return RemoveState.WasRemoved;
                    case RemoveState.ShouldBeRemoved:
                        this._children.RemoveAt(i);
                        return RemoveState.WasRemoved;
                }
            }

            return RemoveState.KeepSearching;
        }
    }
}
