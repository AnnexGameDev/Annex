using Annex.Core.Data;
using Annex.Core.Graphics;
using Scaffold.Collections;

namespace Annex.Core.Scenes.Elements;

public class Container : UIElement, IAddableParentElement
{
    private ConcurrentList<IUIElement> _children = new();

    public Container(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
    }

    public IEnumerable<IUIElement> Children => _children;

    public void AddChild(IUIElement child) {
        this._children.Add(child);
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

            // Look in the child if the child has sub-elements
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

    public T? GetElementById<T>(string id) where T : class, IUIElement {
        return GetElementById(id) as T;
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

        // Otherwise, return ourselves.
        return this;
    }

    public void RemoveChild(string elementId) {
        for (int i = 0; i < this._children.Count; i++)
        {
            if (this._children[i].ElementID == elementId)
            {
                this.RemoveChild(i);
                break;
            }
        }
    }

    public void RemoveChild(IUIElement child) {
        for (int i = 0; i < this._children.Count; i++)
        {
            if (this._children[i] == child)
            {
                this.RemoveChild(i);
                break;
            }
        }
    }

    private void RemoveChild(int i) {
        var child = this._children[i];
        child.Dispose();
        this._children.RemoveAt(i);
    }

    protected override void DrawInternal(ICanvas canvas) {
        foreach (var child in this._children)
        {
            child.DrawOn(canvas);
        }
    }

    protected override void Dispose(bool disposing) {
        base.Dispose(disposing);

        if (disposing)
        {
            for (int i = 0; i < this._children.Count; i++)
            {
                var child = this._children[i];
                child.Dispose();
            }
        }
    }
}
