using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public sealed record SelectIndexChangedEventArgs(int NewIndex, int OldIndex);

public class ListView : Image, IParentElement
{
    public IEnumerable<IUIElement> Children => _children;
    private readonly List<ListViewItem> _children = new();

    private readonly TextureContext _selectionTexture;
    private readonly TextureContext _hoverItemTexture;
    private IVector2<float> _renderOffset = new Vector2f();

    private int _topVisibleIndex;
    private bool _isHoveringAnItem;

    public int LineHeight { get; set; } = 25;
    public int SelectedIndex { get; set; }
    public uint FontSize { get; set; } = 11;
    public RGBA? SelectedFontColor { get; set; }
    public RGBA? FontColor { get; set; } = KnownColor.Black;
    public bool IsSelectable { get; set; } = true;
    public bool ShowIndexPrefix { get; set; }

    public string? SelectedItemTextureId
    {
        get => _selectionTexture.TextureId.Value;
        set => _selectionTexture.TextureId.Set(value ?? string.Empty);
    }

    public string HoverItemTextureId
    {
        get => _hoverItemTexture.TextureId.Value;
        set => _hoverItemTexture.TextureId.Set(value);
    }

    private int _bottomVisibleIndex => _topVisibleIndex + _maxVisibleItemsCount - 1;
    private int _maxVisibleItemsCount => Math.Min((int)(Size.Y / LineHeight), _children.Count);

    public bool HasItemSelected => SelectedIndex >= 0 && SelectedIndex < _children.Count;

    public event EventHandler<SelectIndexChangedEventArgs>? OnSelectedIndexChanged;

    public ListView(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null)
        : base(elementId, position, size) {
        _selectionTexture = new TextureContext(string.Empty.ToShared())
        {
            RenderSize = new Vector2f()
        };
        _hoverItemTexture = new TextureContext(string.Empty.ToShared())
        {
            RenderSize = new Vector2f()
        };
    }

    public IUIElement? GetElementById(string id) {
        if (this.ElementID == id)
        {
            return this;
        }

        for (int i = 0; i < this._children.Count; i++)
        {
            var child = this._children[i];
            if (child.ElementID == id)
            {
                return child;
            }

            if (child is IParentElement childParent)
            {
                var foundElement = childParent.GetElementById(id);
                if (foundElement != null)
                {
                    return foundElement;
                }
            }
        }

        return null;
    }

    public IUIElement? GetFirstVisibleElement(float x, float y) {
        if (!this.IsInBounds(x, y))
            return null;

        for (int i = this._children.Count - 1; i >= 0; i--)
        {
            var child = this._children[i];

            if (!child.Visible)
            {
                continue;
            }

            // Do we hit a sub-child element?
            if (child is IParentElement childParent)
            {
                if (childParent.GetFirstVisibleElement(x, y) is IUIElement hitChild)
                {
                    return hitChild;
                }
            } else
            {
                if (child.IsInBounds(x, y))
                {
                    return child;
                }
            }
        }

        // Otherwise, return oursevles.
        return this;
    }

    public T? GetElementById<T>(string id) where T : class, IUIElement {
        return GetElementById(id) as T;
    }

    protected override void DrawInternal(ICanvas canvas) {
        base.DrawInternal(canvas);

        Debug.Assert(LineHeight != 0, "LineHeight for listview is zero");

        if (HasItemSelected)
        {
            canvas.Draw(_selectionTexture);
        }

        if (_isHoveringAnItem && IsSelectable)
        {
            canvas.Draw(_hoverItemTexture);
        }

        for (int i = _topVisibleIndex; i <= _bottomVisibleIndex; i++)
        {
            var child = _children[i];
            child.DrawOn(canvas);
        }
    }

    public override void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent) {
        base.OnKeyboardKeyPressed(keyboardKeyPressedEvent);
        OnKeyPressed(keyboardKeyPressedEvent.Key);
    }

    private void OnItemHovered(MouseMovedEvent mouseMovedEvent) {
        float y = mouseMovedEvent.WindowY - this.Position.Y;
        int hoveredIndex = _topVisibleIndex + (int)(y / LineHeight);

        if (hoveredIndex >= 0 && hoveredIndex < _children.Count)
        {
            _isHoveringAnItem = true;
            _hoverItemTexture.Position.Set(Position.X, Position.Y + hoveredIndex * LineHeight);
            _hoverItemTexture.RenderSize!.Set(Size.X, LineHeight);
        }
    }

    public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent) {
        base.OnMouseMoved(mouseMovedEvent);

        // If this UI element is hovered, then we're not hovering an item.
        _isHoveringAnItem = false;
    }

    public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent) {
        base.OnMouseLeft(mouseMovedEvent);
        _isHoveringAnItem = false;
    }

    public void AddItem(IShared<string> text) {
        var item = CreateItem(text);
        this._children.Add(item);
    }

    public void Clear() {
        this._children.Clear();
    }

    private ListViewItem CreateItem(IShared<string> text) {
        var item = new ListViewItem(this, new Vector2f(this.Size.X, this.LineHeight), new PrefixedString("", text))
        {
            TrySelectItem = OnItemRequestedTrySelectItem,
            KeyPressed = OnKeyPressed,
        };
        item.SetIndex(this._children.Count);
        return item;
    }

    private void OnKeyPressed(KeyboardKey key) {
        int offset = key switch
        {
            KeyboardKey.Up => -1,
            KeyboardKey.Down => 1,
            _ => 0
        };

        if (offset != 0)
        {
            SelectItem(SelectedIndex + offset);
        }
    }

    private void OnItemRequestedTrySelectItem(int requestedSelectionIndex) {
        if (IsSelectable)
        {
            SelectItem(requestedSelectionIndex);
        }
    }

    private void SelectItem(int index) {
        if (index < 0 || index >= _children.Count)
        {
            return;
        }

        if (!IsSelectable)
        {
            return;
        }

        // Unselect the old item
        int oldIndex = SelectedIndex;
        UnselectItem(oldIndex);

        // Select the new item
        SelectedIndex = index;
        var selectedItem = this._children[SelectedIndex];
        selectedItem.Select();

        // Update the selection background
        RefreshView();
        _selectionTexture.Position.Set(_children[index].Position);
        _selectionTexture.RenderSize!.Set(Size.X, LineHeight);

        var args = new SelectIndexChangedEventArgs(SelectedIndex, oldIndex);
        OnSelectedIndexChanged?.Invoke(this, args);
    }

    private void RefreshView() {
        if (SelectedIndex > _bottomVisibleIndex)
        {
            _topVisibleIndex = SelectedIndex - (_maxVisibleItemsCount - 1);
        }
        if (SelectedIndex < _topVisibleIndex)
        {
            _topVisibleIndex = SelectedIndex;
        }

        _renderOffset.Set(0, -_topVisibleIndex * LineHeight);
    }

    private void UnselectItem(int index) {
        if (index < 0 || index >= _children.Count)
        {
            return;
        }
        var selectedItem = _children[index];
        selectedItem.Unselect();
        SelectedIndex = -1;
    }

    private void ClearSelection() {
        UnselectItem(SelectedIndex);
    }

    private class ListViewItem : Label
    {
        public int Index { get; private set; }
        public bool IsSelected { get; private set; }

        public Action<int>? TrySelectItem { get; set; }
        public Action<KeyboardKey>? KeyPressed { get; set; }
        private ListView _parent;
        private readonly PrefixedString _text;

        public ListViewItem(ListView parent, IVector2<float> itemSize, PrefixedString text)
            : base(
                  position: new OffsetVector2f(new OffsetVector2f(parent.Position, parent._renderOffset), new ScalingVector2f(itemSize, 0, 0)),
                  size: itemSize,
                  textOffset: new ScalingVector2f(itemSize, 0, 0.5f),
                  text: text
                ) {
            HorizontalTextAlignment = HorizontalAlignment.Left;
            VerticalTextAlignment = VerticalAlignment.Middle;
            FontSize = parent.FontSize;
            _parent = parent;
            _text = text;
        }

        internal void SetIndex(int index) {
            Index = index;
            RefreshView();
        }

        private void RefreshView() {
            RefreshPosition();
            FontColor = (IsSelected ? _parent.SelectedFontColor : _parent.FontColor) ?? KnownColor.Black;

            _text.Prefix = _parent.ShowIndexPrefix ? $"{Index}: " : string.Empty;
        }

        private void RefreshPosition() {
            var position = (OffsetVector2f)Position;
            var scale = (ScalingVector2f)position.OffsetVector;
            scale.ScaleVector.Set(0, Index);
        }

        public override void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent) {
            base.OnMouseButtonPressed(mouseButtonPressedEvent);
            TrySelectItem?.Invoke(Index);
        }

        public override void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent) {
            base.OnKeyboardKeyPressed(keyboardKeyPressedEvent);
            KeyPressed?.Invoke(keyboardKeyPressedEvent.Key);
        }

        internal void Unselect() {
            IsSelected = false;
            RefreshView();
        }

        internal void Select() {
            IsSelected = true;
            RefreshView();
        }

        public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent) {
            base.OnMouseMoved(mouseMovedEvent);

            _parent.OnItemHovered(mouseMovedEvent);
        }
    }

    private class PrefixedString : IShared<string>
    {
        private IShared<string> _originalValue;
        public string Prefix { get; set; }

        public string Value
        {
            get => Prefix + _originalValue.Value;
            set => Set(value);
        }

        public PrefixedString(string prefix, IShared<string> value) {
            Prefix = prefix;
            _originalValue = value;
        }

        public void Set(string value) {
            _originalValue.Set(value);
        }
    }
}
