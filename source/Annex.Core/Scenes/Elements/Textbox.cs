using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Hardware;
using Annex.Core.Input;
using Annex.Core.Input.InputEvents;
using Annex.Core.Time;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using Scaffold.Platform;

namespace Annex.Core.Scenes.Elements;

public partial class Textbox : LabeledTextureUIElement, ITextbox
{
    private const long ToggleFrequency = 500;
    private long _nextToggleCursorVisiblity = 0;
    private bool _cursorVisible = false;
    private SolidRectangleContext? _textCursor;
    public int CursorIndex { get; set; }

    private SolidRectangleContext? _selectionHighlight;
    private bool _isSelecting;
    private int _startSelectMouseX;
    private int _endSelectMouseX;
    private ContextMenu? _rightClickContextMenu;
    private int _lastSelectionStart = 0;
    private int _lastSelectionLength = 0;

    private bool _hasSelection => _isSelecting || SelectionLength > 0;

    private readonly IClipboardService _clipboardService;
    private readonly ITimeService _timeService;
    private readonly IGraphicsEngine _graphicsEngine;
    protected readonly IPlatformKeyboardService PlatformKeyboardService;

    public string SelectedText => Text.Substring(SelectionStart, SelectionLength);
    public int SelectionStart { get; private set; }
    public int SelectionLength { get; private set; }

    public Textbox(IContainer container, string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size)
    {
        _clipboardService = container.Resolve<IClipboardService>();
        _timeService = container.Resolve<ITimeService>();
        _graphicsEngine = container.Resolve<IGraphicsEngine>();
        PlatformKeyboardService = container.Resolve<IPlatformKeyboardService>();
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        base.DrawInternal(window, timeDelta);

        UpdateTextSelection();
        if (SelectionLength > 0 && _selectionHighlight != null)
        {
            window.Draw(_selectionHighlight);
        }

        if (!_hasSelection && IsFocused)
        {
            UpdateCursor();
            if (_cursorVisible)
            {
                window.Draw(_textCursor!);
            }
        }
    }

    private void UpdateCursor()
    {
        if (_timeService.ElapsedTimeSince(_nextToggleCursorVisiblity) > ToggleFrequency)
        {
            _cursorVisible = !_cursorVisible;
            _nextToggleCursorVisiblity = _timeService.Now;
        }

        _textCursor ??= new SolidRectangleContext(KnownColor.Black, new Vector2f(), new Vector2f())
        {
            Camera = KnownCamera.UI
        };

        float x = _graphicsEngine.GetCharacterX(Label.RenderText, CursorIndex, forceContextUpdate: true);
        _textCursor.Position.Set(Position.X + x, Position.Y);
        _textCursor.Size.Set(1, Size.Y);
    }

    // Converts the selectedX's to actual indices
    private void UpdateTextSelection_FromMouseEvent()
    {
        float startMouseX = Math.Min(_startSelectMouseX, _endSelectMouseX) - Position.X;
        float endMouseX = Math.Max(_startSelectMouseX, _endSelectMouseX) - Position.X;

        var ctx = Label.RenderText;
        _graphicsEngine.GetTextBounds(ctx, forceContextUpdate: true); // force a context update

        int startSelectIndex = 0;
        int endSelectIndex = 0;

        for (int i = 0; i <= Text.Length; i++)
        {
            float x = _graphicsEngine.GetCharacterX(ctx, i, forceContextUpdate: false);

            if (startMouseX >= x)
            {
                startSelectIndex = i;
            }
            if (endMouseX >= x)
            {
                endSelectIndex = i;
            }
        }

        SelectionStart = startSelectIndex;
        SelectionLength = endSelectIndex - startSelectIndex;
    }

    // Updates the selection context from the indices
    private void UpdateTextSelection()
    {
        if (SelectionStart < 0)
        {
            SelectionStart = 0;
        }
        int maxPossibleSelection = Text.Length - SelectionStart;
        if (SelectionLength > maxPossibleSelection)
        {
            SelectionLength = maxPossibleSelection;
        }

        if (_lastSelectionStart == SelectionStart && _lastSelectionLength == SelectionLength)
        {
            return;
        }
        _lastSelectionStart = SelectionStart;
        _lastSelectionLength = SelectionLength;

        _selectionHighlight ??= new SolidRectangleContext(new RGBA(0, 0, 255, 100), new Vector2f(), new Vector2f())
        {
            Camera = KnownCamera.UI
        };

        var ctx = Label.RenderText;
        float startX = _graphicsEngine.GetCharacterX(ctx, SelectionStart, forceContextUpdate: true);
        float endX = _graphicsEngine.GetCharacterX(ctx, SelectionStart + SelectionLength, forceContextUpdate: false);

        _selectionHighlight.Position.Set(Position.X + startX, Position.Y);
        _selectionHighlight.Size.Set(endX - startX, Size.Y);
    }

    public override void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent)
    {
        base.OnMouseButtonReleased(mouseButtonReleasedEvent);

        if (mouseButtonReleasedEvent.Button == MouseButton.Right)
        {
            _rightClickContextMenu?.RemoveFromCurrentScene();

            _rightClickContextMenu = new ContextMenu(
                new Vector2f(mouseButtonReleasedEvent.WindowX, mouseButtonReleasedEvent.WindowY),
                new ContextMenu.Item("Cut", CutSelectedText),
                new ContextMenu.Item("Copy", CopySelectedText),
                new ContextMenu.Item("Paste", PasteText)
            );
            _rightClickContextMenu.AddToScene(mouseButtonReleasedEvent.Window.Scene);
        }

        if (mouseButtonReleasedEvent.Button == MouseButton.Left)
        {
            TryStopSelecting();
        }
    }

    public override void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent)
    {
        base.OnMouseButtonPressed(mouseButtonPressedEvent);

        if (mouseButtonPressedEvent.Button == MouseButton.Left)
        {
            _isSelecting = true;
            _startSelectMouseX = (int)mouseButtonPressedEvent.WindowX;
            _endSelectMouseX = (int)mouseButtonPressedEvent.WindowX;
            UpdateTextSelection_FromMouseEvent();
        }

        if (mouseButtonPressedEvent.Button == MouseButton.Left || mouseButtonPressedEvent.Button == MouseButton.Right)
        {
            TryCloseContextMenu();
        }
    }

    public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseMoved(mouseMovedEvent);

        if (_isSelecting)
        {
            _endSelectMouseX = (int)mouseMovedEvent.WindowX;
            UpdateTextSelection_FromMouseEvent();
        }
    }

    public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseLeft(mouseMovedEvent);
        TryStopSelecting();
    }

    public override void OnLostFocus()
    {
        base.OnLostFocus();
        TryCloseContextMenu();
        ClearSelectText();
    }

    public void ClearSelectText()
    {
        SelectionStart = 0;
        SelectionLength = 0;
        _selectionHighlight?.Dispose();
        _selectionHighlight = null;
    }

    public void SelectText(int start, int length)
    {
        SelectionStart = start;
        SelectionLength = length;
        UpdateTextSelection();
    }

    public override void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent)
    {
        base.OnKeyboardKeyPressed(keyboardKeyPressedEvent);

        if (keyboardKeyPressedEvent.Key == KeyboardKey.X)
        {
            if (PlatformKeyboardService.IsControlPressed())
            {
                CutSelectedText(keyboardKeyPressedEvent);
                return;
            }
        }

        if (keyboardKeyPressedEvent.Key == KeyboardKey.C)
        {
            if (PlatformKeyboardService.IsControlPressed())
            {
                CopySelectedText(keyboardKeyPressedEvent);
                return;
            }
        }

        if (keyboardKeyPressedEvent.Key == KeyboardKey.V)
        {
            if (PlatformKeyboardService.IsControlPressed())
            {
                PasteText(keyboardKeyPressedEvent);
                return;
            }
        }

        if (keyboardKeyPressedEvent.Key == KeyboardKey.A)
        {
            if (PlatformKeyboardService.IsControlPressed())
            {
                CursorIndex = 0;
                SelectionStart = 0;
                SelectionLength = Text.Length;
                return;
            }
        }

        // TODO: What happens if shift is pressed
        if (keyboardKeyPressedEvent.Key == KeyboardKey.Left)
        {
            _cursorVisible = true;
            CursorIndex = Math.Max(0, CursorIndex - 1);
            return;
        }
        if (keyboardKeyPressedEvent.Key == KeyboardKey.Right)
        {
            _cursorVisible = true;
            CursorIndex = Math.Min(Text.Length, CursorIndex + 1);
            return;
        }

        var content = keyboardKeyPressedEvent.LiteralContent;

        // If there's no selection, we need to handle backspace and delete differently.
        if (!_hasSelection)
        {
            if (keyboardKeyPressedEvent.Key == KeyboardKey.BackSpace)
            {
                // Nothing to backspace?
                if (CursorIndex == 0)
                {
                    return;
                }

                int removeStart = CursorIndex - 1;
                int removeLength = 1;

                // Do we delete a chunk?
                if (keyboardKeyPressedEvent.IsControlPressed)
                {
                    for (; removeStart > 0; removeStart--)
                    {
                        if (char.IsWhiteSpace(Text[removeStart]))
                        {
                            break;
                        }
                    }
                    removeLength = CursorIndex - removeStart;
                }

                Text = Text.Remove(removeStart, removeLength);
                CursorIndex -= removeLength;
                return;
            }

            if (keyboardKeyPressedEvent.Key == KeyboardKey.Delete)
            {
                // Nothing to delete?
                if (CursorIndex == Text.Length)
                {
                    return;
                }
                Text = Text.Remove(CursorIndex, 1);
                return;
            }
        }

        // Should we add content if it's empty?
        if (content.Length == 0)
        {
            if (keyboardKeyPressedEvent.Key != KeyboardKey.Backspace && keyboardKeyPressedEvent.Key != KeyboardKey.Delete)
            {
                return;
            }
        }

        AddTextAtCursorOrSelection(keyboardKeyPressedEvent.LiteralContent);
    }

    private void AddTextAtCursorOrSelection(string text)
    {
        _cursorVisible = true;

        if (CursorIndex < 0 || CursorIndex > Text.Length)
        {
            Log.Error($"{nameof(CursorIndex)} for Textbox with content {Text} is out of range: {CursorIndex}");
            return;
        }

        if (_hasSelection)
        {

            Text = Text.Remove(SelectionStart, SelectionLength);
            CursorIndex = SelectionStart;
            ClearSelectText();
        }

        // Replace as normal.
        Text = Text.Insert(CursorIndex, text);
        CursorIndex += text.Length;
    }

    private void TryCloseContextMenu()
    {
        _rightClickContextMenu?.RemoveFromCurrentScene();
        _rightClickContextMenu = null;
    }

    private void TryStopSelecting()
    {
        if (!_isSelecting)
        {
            return;
        }
        _isSelecting = false;
        UpdateTextSelection_FromMouseEvent();

        // Take advantage of text selection above to compute the cursor index
        CursorIndex = SelectionStart;
        _cursorVisible = true;
        _nextToggleCursorVisiblity = _timeService.Now;
    }


    #region Clipboard actions
    private void PasteText(WindowEvent windowEvent)
    {
        AddTextAtCursorOrSelection(_clipboardService.GetString() ?? string.Empty);
    }

    private void CopySelectedText(WindowEvent windowEvent)
    {
        _clipboardService.SetString(SelectedText);
    }

    private void CutSelectedText(WindowEvent windowEvent)
    {
        _clipboardService.SetString(SelectedText);
        Text = Text.Remove(SelectionStart, SelectionLength);
    }
    #endregion
}
