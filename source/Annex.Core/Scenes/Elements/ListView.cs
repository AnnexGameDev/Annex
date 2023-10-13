using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Scenes.Elements;

public class ListView : Image, IParentElement
{
    public IEnumerable<IUIElement> Children => _children;
    private readonly List<ListViewItem> _children = new();

    public int LineHeight
    {
        get;
        set;
    }

    public uint FontSize
    {
        get;
        set;
    }

    public RGBA FontColor
    {
        get;
        set;
    }

    public ListView(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null)
        : base(elementId, position, size) {
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
        var item = new ListViewItem(this, new Vector2f(this.Size.X, this.LineHeight), text);
        item.SetIndex(this._children.Count);
        return item;
    }

    private class ListViewItem : Label
    {
        private int _index;
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
            _index = index;
            RefreshView();
        }

        private void RefreshView() {
            RefreshPosition();
            FontColor = _parent.FontColor;
        }

        private void RefreshPosition() {
            var position = (OffsetVector2f)Position;
            var scale = (ScalingVector2f)position.OffsetVector;
            scale.ScaleVector.Set(0, _index);
        }
    }
}
