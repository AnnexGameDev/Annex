namespace Annex.Core.Scenes.Elements;

public interface IAddableParentElement : IParentElement
{
    void AddChild(IUIElement child);

    void RemoveChild(string elementId);
    void RemoveChild(IUIElement child);
}
