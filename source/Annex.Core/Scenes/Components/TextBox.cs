using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input;
using Annex.Core.Input.InputEvents;
using Annex.Core.Platform;

namespace Annex.Core.Scenes.Components
{
    public partial class TextBox : LabeledTextureUIElement, ITextbox
    {
        private ContextMenu? _rightClickContextMenu;
        private SolidRectangleContext? _selectionHighlight;
        private bool _isSelecting;
        private int _startSelectMouseX;
        private int _endSelectMouseX;

        public string SelectedText => this.Text.Substring(this.SelectionStart, this.SelectionLength);
        public int SelectionStart { get; private set; }
        public int SelectionLength { get; private set; }

        public TextBox(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
        }

        protected override void DrawInternal(ICanvas canvas) {
            base.DrawInternal(canvas);

            this.UpdateTextSelection();
            if (this.SelectionLength > 0 && this._selectionHighlight != null) {
                canvas.Draw(this._selectionHighlight);
            }

            this._rightClickContextMenu?.Draw(canvas);
        }

        // Converts the selectedX's to actual indices
        private void UpdateTextSelection_FromMouseEvent() {
            float startMouseX = Math.Min(this._startSelectMouseX, this._endSelectMouseX) - this.Position.X;
            float endMouseX = Math.Max(this._startSelectMouseX, this._endSelectMouseX) - this.Position.X;

            var ctx = this.Label.RenderText;
            GraphicsEngine.GetTextBounds(ctx, forceContextUpdate: true); // force a context update

            int startSelectIndex = 0;
            int endSelectIndex = 0;

            for (int i = 0; i <= this.Text.Length; i++) {
                float x = GraphicsEngine.GetCharacterX(ctx, i, forceContextUpdate: false);

                if (startMouseX >= x) {
                    startSelectIndex = i;
                }
                if (endMouseX >= x) {
                    endSelectIndex = i;
                }
            }

            this.SelectionStart = startSelectIndex;
            this.SelectionLength = endSelectIndex - startSelectIndex;
        }

        // Updates the selection context from the indices
        private void UpdateTextSelection() {
            if (this.SelectionStart < 0) {
                this.SelectionStart = 0;
            }
            int maxPossibleSelection = this.Text.Length - this.SelectionStart;
            if (this.SelectionLength > maxPossibleSelection) {
                this.SelectionLength = maxPossibleSelection;
            }

            this._selectionHighlight ??= new SolidRectangleContext(new RGBA(0, 0, 255, 100), new Vector2f(), new Vector2f());

            var ctx = this.Label.RenderText;
            float startX = GraphicsEngine.GetCharacterX(ctx, this.SelectionStart, forceContextUpdate: true);
            float endX = GraphicsEngine.GetCharacterX(ctx, this.SelectionStart + this.SelectionLength, forceContextUpdate: false);

            this._selectionHighlight.Position.Set(this.Position.X + startX, this.Position.Y);
            this._selectionHighlight.Size.Set(endX - startX, this.Size.Y);
        }

        public override void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent) {
            base.OnMouseButtonReleased(mouseButtonReleasedEvent);

            if (mouseButtonReleasedEvent.Button == MouseButton.Right) {
                this._rightClickContextMenu ??= new ContextMenu(
                    new Vector2f(mouseButtonReleasedEvent.WindowX, mouseButtonReleasedEvent.WindowY),
                    new ContextMenu.Item("Cut", CutSelectedText),
                    new ContextMenu.Item("Copy", CopySelectedText),
                    new ContextMenu.Item("Paste", PasteText)
                );
            }

            if (mouseButtonReleasedEvent.Button == MouseButton.Left) {
                this._isSelecting = false;
                this.UpdateTextSelection_FromMouseEvent();
            }
        }

        public override void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent) {
            base.OnMouseButtonPressed(mouseButtonPressedEvent);

            if (mouseButtonPressedEvent.Button == MouseButton.Left) {
                this._isSelecting = true;
                this._startSelectMouseX = mouseButtonPressedEvent.WindowX;
                this._endSelectMouseX = mouseButtonPressedEvent.WindowX;
                this.UpdateTextSelection_FromMouseEvent();
            }
        }

        public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent) {
            base.OnMouseMoved(mouseMovedEvent);

            if (this._isSelecting) {
                this._endSelectMouseX = mouseMovedEvent.WindowX;
                this.UpdateTextSelection_FromMouseEvent();
            }
        }

        public override void OnLostFocus() {
            base.OnLostFocus();
            this._rightClickContextMenu?.Dispose();
            this._rightClickContextMenu = null;
            this.ClearSelectText();
        }

        public void ClearSelectText() {
            this.SelectionStart = 0;
            this.SelectionLength = 0;
            this._selectionHighlight?.Dispose();
            this._selectionHighlight = null;
        }

        public void SelectText(int start, int length) {
            this.SelectionStart = start;
            this.SelectionLength = length;
            this.UpdateTextSelection();
        }

        #region Clipboard actions
        private void PasteText() {
            this.Text = Clipboard.GetString() ?? string.Empty;
        }

        private void CopySelectedText() {
            Clipboard.SetString(this.SelectedText);
        }

        private void CutSelectedText() {
            Clipboard.SetString(this.SelectedText);
            this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength);
        }
        #endregion
    }

    public partial class TextBox : IParentElement
    {
        public IEnumerable<IUIElement> Children => GetChildren();

        private IEnumerable<IUIElement> GetChildren() {
            if (this._rightClickContextMenu != null) {
                yield return this._rightClickContextMenu;
            }
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
