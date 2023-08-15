namespace Annex.Core.Scenes.Elements;

public interface IParentElement : IUIElement
{
    IEnumerable<IUIElement> Children { get; }
    void AddChild(IUIElement child);
    
    void RemoveChild(string elementId);
    void RemoveChild(IUIElement child);

    IUIElement? GetElementById(string id);
    IUIElement? GetFirstVisibleElement(float x, float y);
}
