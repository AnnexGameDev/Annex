#nullable enable
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Scenes.Controllers;

namespace Annex.Scenes.Components
{
    public abstract class UIElement : InputController, IDrawableObject
    {
        private readonly string ElementID;
        public readonly Vector Size;
        public readonly Vector Position;
        public bool IsFocus { get; internal set; }
        public bool Visible;

        public UIElement(string elementID) {
            this.ElementID = elementID;
            this.Size = Vector.Create(100, 100);
            this.Position = Vector.Create(0, 0);
            this.Visible = true;
        }

        public abstract void Draw(IDrawableContext context);

        public virtual UIElement? GetElementById(string id) {
            if (this.ElementID == id) {
                return this;
            }
            return null;
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
