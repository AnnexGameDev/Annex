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

        public float Top => this.Position.Y;
        public float Left => this.Position.X;
        public float Right => this.Position.X + this.Size.X;
        public float Bottom => this.Position.Y + this.Size.Y;

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

        public bool IsMouseWithinElementBounds() {
            var canvas = ServiceProvider.Canvas;

            if (!canvas.IsActive) {
                return false;
            }

            var mousePos = canvas.GetRealMousePos();

            if (mousePos.X < this.Left || mousePos.X > this.Right) {
                return false;
            }
            if (mousePos.Y < this.Top || mousePos.Y > this.Bottom) {
                return false;
            }

            return true;
        }

        internal override bool HandleSceneFocusMouseDown(int x, int y) {
            if (!this.Visible) {
                return false;
            }
            if (x < this.Left || x > this.Right) {
                return false;
            }
            if (y < this.Top || y > this.Bottom) {
                return false;
            }
            this.IsFocus = true;
            ServiceProvider.SceneService.CurrentScene.FocusObject = this;
            return true;
        }
    }
}
