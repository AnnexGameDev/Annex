﻿using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Graphics.Contexts;
using Annex_Old.Core.Helpers;
using Annex_Old.Core.Input;
using Annex_Old.Core.Input.InputEvents;
using Scaffold.Logging;

namespace Annex_Old.Core.Scenes.Components
{
    public partial class TextBox : LabeledTextureUIElement, ITextbox
    {
        private ContextMenu? _rightClickContextMenu;

        private const long ToggleFrequency = 500;
        private long _nextToggleCursorVisiblity = 0;
        private bool _cursorVisible = false;
        private SolidRectangleContext? _textCursor;
        public int CursorIndex { get; set; }

        private SolidRectangleContext? _selectionHighlight;
        private bool _isSelecting;
        private int _startSelectMouseX;
        private int _endSelectMouseX;
        private bool _hasSelection => this._isSelecting || this.SelectionLength > 0;

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

            if (!this._hasSelection && this.IsFocused) {
                this.UpdateCursor();
                if (this._cursorVisible) {
                    canvas.Draw(this._textCursor!);
                }
            }

            this._rightClickContextMenu?.Draw(canvas);
        }

        private void UpdateCursor() {
            if (GameTimeHelper.ElapsedTimeSince(this._nextToggleCursorVisiblity) > ToggleFrequency) {
                this._cursorVisible = !this._cursorVisible;
                this._nextToggleCursorVisiblity = GameTimeHelper.Now();
            }

            this._textCursor ??= new SolidRectangleContext(KnownColor.Black, new Vector2f(), new Vector2f()) {
                Camera = CameraId.UI.ToString()
            };

            float x = GraphicsEngineHelper.GetCharacterX(this.Label.RenderText, this.CursorIndex, forceContextUpdate: true);
            this._textCursor.Position.Set(this.Position.X + x, this.Position.Y);
            this._textCursor.Size.Set(1, this.Size.Y);
        }

        // Converts the selectedX's to actual indices
        private void UpdateTextSelection_FromMouseEvent() {
            float startMouseX = Math.Min(this._startSelectMouseX, this._endSelectMouseX) - this.Position.X;
            float endMouseX = Math.Max(this._startSelectMouseX, this._endSelectMouseX) - this.Position.X;

            var ctx = this.Label.RenderText;
            GraphicsEngineHelper.GetTextBounds(ctx, forceContextUpdate: true); // force a context update

            int startSelectIndex = 0;
            int endSelectIndex = 0;

            for (int i = 0; i <= this.Text.Length; i++) {
                float x = GraphicsEngineHelper.GetCharacterX(ctx, i, forceContextUpdate: false);

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

            this._selectionHighlight ??= new SolidRectangleContext(new RGBA(0, 0, 255, 100), new Vector2f(), new Vector2f()) {
                Camera = CameraId.UI.ToString()
            };

            var ctx = this.Label.RenderText;
            float startX = GraphicsEngineHelper.GetCharacterX(ctx, this.SelectionStart, forceContextUpdate: true);
            float endX = GraphicsEngineHelper.GetCharacterX(ctx, this.SelectionStart + this.SelectionLength, forceContextUpdate: false);

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

                // Take advantage of text selection above to compute the cursor index
                this.CursorIndex = this.SelectionStart;
                this._cursorVisible = true;
                this._nextToggleCursorVisiblity = GameTimeHelper.Now();
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

            if (mouseButtonPressedEvent.Button == MouseButton.Left || mouseButtonPressedEvent.Button == MouseButton.Right) {
                this.CloseContextMenu();
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
            this.CloseContextMenu();
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

        public override void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent) {
            base.OnKeyboardKeyPressed(keyboardKeyPressedEvent);

            if (keyboardKeyPressedEvent.Key == KeyboardKey.X) {
                if (KeyboardHelper.IsControlPressed()) {
                    this.CutSelectedText();
                    return;
                }
            }

            if (keyboardKeyPressedEvent.Key == KeyboardKey.C) {
                if (KeyboardHelper.IsControlPressed()) {
                    this.CopySelectedText();
                    return;
                }
            }

            if (keyboardKeyPressedEvent.Key == KeyboardKey.V) {
                if (KeyboardHelper.IsControlPressed()) {
                    this.PasteText();
                    return;
                }
            }

            if (keyboardKeyPressedEvent.Key == KeyboardKey.A) {
                if (KeyboardHelper.IsControlPressed()) {
                    this.CursorIndex = 0;
                    this.SelectionStart = 0;
                    this.SelectionLength = this.Text.Length;
                    return;
                }
            }

            // TODO: What happens if shift is pressed
            if (keyboardKeyPressedEvent.Key == KeyboardKey.Left) {
                this._cursorVisible = true;
                this.CursorIndex = Math.Max(0, this.CursorIndex - 1);
                return;
            }
            if (keyboardKeyPressedEvent.Key == KeyboardKey.Right) {
                this._cursorVisible = true;
                this.CursorIndex = Math.Min(this.Text.Length, this.CursorIndex + 1);
                return;
            }

            var content = keyboardKeyPressedEvent.LiteralContent;

            // If there's no selection, we need to handle backspace and delete differently.
            if (!this._hasSelection) {
                if (keyboardKeyPressedEvent.Key == KeyboardKey.BackSpace) {

                    // Nothing to backspace?
                    if (this.CursorIndex == 0) {
                        return; 
                    }

                    this.Text = this.Text.Remove(this.CursorIndex - 1, 1);
                    this.CursorIndex--;
                    return;
                }

                if (keyboardKeyPressedEvent.Key == KeyboardKey.Delete) {
                    // Nothing to delete?
                    if (this.CursorIndex == this.Text.Length) {
                        return;
                    }
                    this.Text = this.Text.Remove(this.CursorIndex, 1);
                    return;
                }
            }

            // Should we add content if it's empty?
            if (content.Length == 0) {
                if (keyboardKeyPressedEvent.Key != KeyboardKey.Backspace && keyboardKeyPressedEvent.Key != KeyboardKey.Delete) {
                    return;
                }
            }

            AddTextAtCursorOrSelection(keyboardKeyPressedEvent.LiteralContent);
        }

        private void AddTextAtCursorOrSelection(string text) {
            this._cursorVisible = true;

            if (this.CursorIndex < 0 || this.CursorIndex > this.Text.Length) {
                Log.Trace(LogSeverity.Error, $"{nameof(this.CursorIndex)} for Textbox with content {this.Text} is out of range: {this.CursorIndex}");
                return;
            }

            if (this._hasSelection) {

                this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength);
                this.CursorIndex = this.SelectionStart;
                this.ClearSelectText();
            }

            // Replace as normal.
            this.Text = this.Text.Insert(this.CursorIndex, text);
            this.CursorIndex += text.Length;
        }

        private void CloseContextMenu() {
            this._rightClickContextMenu?.Dispose();
            this._rightClickContextMenu = null;
        }

        #region Clipboard actions
        private void PasteText() {
            AddTextAtCursorOrSelection(ClipboardHelper.GetString() ?? string.Empty);
        }

        private void CopySelectedText() {
            ClipboardHelper.SetString(this.SelectedText);
        }

        private void CutSelectedText() {
            ClipboardHelper.SetString(this.SelectedText);
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
