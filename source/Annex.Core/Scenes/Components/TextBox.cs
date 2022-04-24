using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Input.InputEvents;
using Annex.Core.Platform;

namespace Annex.Core.Scenes.Components
{
    public class TextBox : LabeledTextureUIElement, IParentElement
    {
        private ContextMenu? _rightClickContextMenu;

        public IEnumerable<IUIElement> Children => throw new NotImplementedException();

        public TextBox(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
        }

        protected override void DrawInternal(ICanvas canvas) {
            base.DrawInternal(canvas);
            this._rightClickContextMenu?.Draw(canvas);
        }

        public override void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent) {
            base.OnMouseButtonReleased(mouseButtonReleasedEvent);
            if (mouseButtonReleasedEvent.Button == Input.MouseButton.Right) {
                this._rightClickContextMenu ??= new ContextMenu(
                    new Vector2f(mouseButtonReleasedEvent.WindowX, mouseButtonReleasedEvent.WindowY),
                    new ContextMenu.Item("Cut", CutSelectedText),
                    new ContextMenu.Item("Copy", CopySelectedText),
                    new ContextMenu.Item("Paste", PasteText)
                );
            }
        }

        public override void OnLostFocus() {
            base.OnLostFocus();
            this._rightClickContextMenu = null;
        }

        private void PasteText() {
            this.Text = Clipboard.GetString() ?? String.Empty;
        }

        private void CopySelectedText() {
            Clipboard.SetString(this.Text);
        }

        private void CutSelectedText() {
            Clipboard.SetString(this.Text);
            this.Text = string.Empty;
        }

        public void AddChild(IUIElement child) {
            throw new NotSupportedException($"Unable to add children to a textbox");
        }

        public IUIElement? GetElementById(string id) {
            if (id == this.ElementID) 
                return this;
            return this._rightClickContextMenu?.GetElementById(id);
        }

        public IUIElement? GetFirstVisibleElement(float x, float y) {
            if (this._rightClickContextMenu?.GetFirstVisibleElement(x, y) is IUIElement child) {
                return child;
            }

            if (this.IsInBounds(x, y)) {
                return this;
            }

            return null;
        }
    }
}
