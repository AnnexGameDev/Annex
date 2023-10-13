using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class ListView : Image, IParentElement
{
    public IEnumerable<IUIElement> Children => _children;
    private readonly List<ListViewItem> _children = new();

    private readonly TextureContext _selectionTexture;

    public int LineHeight { get; set; }
    public int SelectedIndex { get; set; }
    public uint FontSize { get; set; }
    public RGBA? SelectedFontColor { get; set; }
    public RGBA? FontColor { get; set; }
    public bool IsSelectable { get; set; }

    public string? SelectedTextureId
    {
        get => _selectionTexture.TextureId.Value;
        set => _selectionTexture.TextureId.Set(value ?? string.Empty);
    }

    public bool HasItemSelected => SelectedIndex >= 0 && SelectedIndex < _children.Count;

    public ListView(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null)
        : base(elementId, position, size) {
        _selectionTexture = new TextureContext(string.Empty.ToShared())
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

        if (HasItemSelected)
        {
            canvas.Draw(_selectionTexture);
        }

        for (int i = 0; i < this._children.Count; i++)
        {
            var child = _children[i];
            child.Draw(canvas);
        }
    }

    public void AddItem(IShared<string> text) {
        var item = CreateItem(text);
        this._children.Add(item);
    }

    private ListViewItem CreateItem(IShared<string> text) {
        var item = new ListViewItem(this, new Vector2f(this.Size.X, this.LineHeight), text)
        {
            OnClicked = OnListViewItemClicked
        };
        item.SetIndex(this._children.Count);
        return item;
    }

    private void OnListViewItemClicked(ListViewItem item) {
        if (IsSelectable)
        {
            SelectItem(item.Index);
        }
    }

    private void SelectItem(int index) {
        if (index < 0 || index >= _children.Count)
        {
            ClearSelection();
            return;
        }

        // Unselect the old item
        UnselectItem(SelectedIndex);

        // Select the new item
        SelectedIndex = index;
        var selectedItem = this._children[SelectedIndex];
        selectedItem.Select();

        // Update the selection background
        _selectionTexture.Position.Set(_children[index].Position);
        _selectionTexture.RenderSize!.Set(Size.X, LineHeight);
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

        public Action<ListViewItem>? OnClicked { get; set; }

        private ListView _parent;

        public ListViewItem(ListView parent, IVector2<float> itemSize, IShared<string> text)
            : base(
                  position: new OffsetVector2f(parent.Position, new ScalingVector2f(itemSize, 0, 0)),
                  size: itemSize,
                  textOffset: new ScalingVector2f(itemSize, 0, 0.5f),
                  text: text
                ) {
            HorizontalTextAlignment = HorizontalAlignment.Left;
            VerticalTextAlignment = VerticalAlignment.Middle;
            FontSize = parent.FontSize;
            _parent = parent;
        }

        internal void SetIndex(int index) {
            Index = index;
            RefreshView();
        }

        private void RefreshView() {
            RefreshPosition();
            FontColor = (IsSelected ? _parent.SelectedFontColor : _parent.FontColor) ?? KnownColor.Black;
        }

        private void RefreshPosition() {
            var position = (OffsetVector2f)Position;
            var scale = (ScalingVector2f)position.OffsetVector;
            scale.ScaleVector.Set(0, Index);
        }

        public override void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent) {
            base.OnMouseButtonPressed(mouseButtonPressedEvent);
            OnClicked?.Invoke(this);
        }

        internal void Unselect() {
            IsSelected = false;
            RefreshView();
        }

        internal void Select() {
            IsSelected = true;
            RefreshView();
        }
    }
}
