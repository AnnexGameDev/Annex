using Annex.Graphics;
using Annex.Graphics.Events;
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

        public override UIElement? GetFirstVisibleChildElementAt(int x, int y) {
            // z-index from last to first.
            for (int i = this._children.Count - 1; i >= 0; i--) {
                var child = this._children[i];

                if (!child.Visible) {
                    continue;
                }
                if (child.GetFirstVisibleChildElementAt(x, y) is UIElement visibleChild) {
                    return visibleChild;
                }
            }
            return base.GetFirstVisibleChildElementAt(x, y);
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

        public override void HandleMouseMoved(MouseMovedEvent e) {

            for (int i = 0; i < this._children.Count; i++) {
                var child = this._children[i];
                var x = e.MouseX;
                var y = e.MouseY;

                if (x >= child.Position.X && x <= child.Position.X + child.Size.X) {
                    if (y >= child.Position.Y && y <= child.Position.Y + child.Size.Y) {
                        if (!child.WasPreviouslyHoveredOver) {
                            child.OnMouseEntered();
                        }
                        child.HandleMouseMoved(e);
                        continue;
                    }
                }

                if (child.WasPreviouslyHoveredOver) {
                    child.OnMouseLeft();
                }
            }
        }

        public override void OnMouseLeft() {
            base.OnMouseLeft();

            for (int i = 0; i< this._children.Count; i++) {
                var child = this._children[i];

                if (child.WasPreviouslyHoveredOver) {
                    child.OnMouseLeft();
                }
            }
        }
    }
}
