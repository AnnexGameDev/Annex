using Annex.Core.Data;
using Annex.Core.Graphics.Windows;
using Scaffold.Collections;
using System.Runtime.CompilerServices;

namespace Annex.Core.Scenes.Elements;

public class Container : UIElement, IAddableParentElement
{
    private ConcurrentList<IUIElement> _children = new();

    public Container(UIElementCreationArgs? args) : base(args)
    {
    }

    public IEnumerable<IUIElement> Children => _children;

    public virtual void AddChild(IUIElement child)
    {
        _children.Add(child);
    }

    public T GetElement<T>([CallerMemberName] string elementId = "") where T : class, IUIElement
    {
        return GetElementById<T>(elementId) ?? throw new UIElementNotFoundException(elementId);
    }

    public IUIElement? GetElementById(string id)
    {
        if (ElementID == id)
        {
            return this;
        }

        for (int i = 0; i < _children.Count; i++)
        {
            var child = _children[i];
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

    public T? GetElementById<T>(string id) where T : class, IUIElement
    {
        return GetElementById(id) as T;
    }

    public IUIElement? GetFirstVisibleElement(float x, float y)
    {

        if (!IsInBounds(x, y))
            return null;

        for (int i = _children.Count - 1; i >= 0; i--)
        {
            var child = _children[i];

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
            }
            else
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

    public virtual void RemoveChild(string elementId)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            if (_children[i].ElementID == elementId)
            {
                RemoveChild(i);
                break;
            }
        }
    }

    public void RemoveChild(IUIElement child)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            if (_children[i] == child)
            {
                RemoveChild(i);
                break;
            }
        }
    }

    private void RemoveChild(int i)
    {
        var child = _children[i];
        child.Dispose();
        _children.RemoveAt(i);
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            _children[i]?.DrawOn(window, timeDelta);
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];
                child.Dispose();
            }
        }
    }
}
