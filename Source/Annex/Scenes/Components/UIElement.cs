#nullable enable
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Scenes.Controllers;

namespace Annex.Scenes.Components
{
    public enum RemoveState
    {
        KeepSearching,
        ShouldBeRemoved,
        WasRemoved
    }

    public abstract class UIElement : InputController, IDrawableObject
    {
        private protected readonly string ElementID;
        public readonly Vector Size;
        public readonly Vector Position;
        public bool IsFocus { get; internal set; }
        public bool Visible;

        public UIElement(string elementID) {
            this.ElementID = elementID;
            this.Size = Vector.Create(100, 100);
            this.Position = Vector.Create();
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

        internal override bool HandleSceneFocusMouseDown(int x, int y) {
            if (!this.Visible) {
                return false;
            }
            if (x < this.Position.X) {
                return false;
            }
            if (y < this.Position.Y) {
                return false;
            }
            if (x > this.Position.X + this.Size.X) {
                return false;
            }
            if (y > this.Position.Y + this.Size.Y) {
                return false;
            }
            this.IsFocus = true;
            SceneManager.Singleton.CurrentScene.FocusObject = this;
            return true;
        }
    }
}
