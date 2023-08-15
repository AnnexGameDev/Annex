using Annex_Old.Data.Shared;
using Annex_Old.Graphics;
using Annex_Old.Graphics.Events;
using Annex_Old.Scenes.Controllers;
using System;

namespace Annex_Old.Scenes.Components
{
    public enum RemoveState
    {
        KeepSearching,
        ShouldBeRemoved,
        WasRemoved
    }

    public abstract class UIElement : InputController, IDrawableObject
    {
        public readonly string ElementID;
        public readonly Vector Size;
        public readonly Vector Position;
        public bool HasFocus { get; private set; }
        public bool Visible;

        internal bool WasPreviouslyHoveredOver = false;

        public UIElement(string elementID,
            Vector? size = null,
            Vector? position = null) {
            this.ElementID = elementID;
            this.Size = size ?? Vector.Create(100, 100);
            this.Position = position ?? Vector.Create();
            this.Visible = true;
        }

        public abstract void Draw(ICanvas canvas);

        public virtual UIElement? GetElementById(string id) {
            if (this.ElementID == id) {
                return this;
            }
            return null;
        }

        public virtual RemoveState RemoveElementById(string id) {
            if (this.ElementID == id) {
                return RemoveState.ShouldBeRemoved;
            }
            return RemoveState.KeepSearching;
        }

        public override UIElement? GetFirstVisibleChildElementAt(int x, int y) {
            if (!this.Visible) {
                return null;
            }
            if (x < this.Position.X) {
                return null;
            }
            if (y < this.Position.Y) {
                return null;
            }
            if (x > this.Position.X + this.Size.X) {
                return null;
            }
            if (y > this.Position.Y + this.Size.Y) {
                return null;
            }
            return this;
        }

        public virtual void GainedFocus() {
            this.HasFocus = true;
        }

        public virtual void LostFocus() {
            this.HasFocus = false;
        }

        public virtual void OnMouseEntered() {
            this.WasPreviouslyHoveredOver = true;
        }

        public virtual void OnMouseLeft() {
            this.WasPreviouslyHoveredOver = false;
        }
    }
}
