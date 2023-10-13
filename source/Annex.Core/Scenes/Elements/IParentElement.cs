namespace Annex.Core.Scenes.Elements;

public interface IParentElement : IUIElement
{
    IEnumerable<IUIElement> Children { get; }

    T? GetElementById<T>(string id) where T : class, IUIElement;
    IUIElement? GetElementById(string id);
    IUIElement? GetFirstVisibleElement(float x, float y);
}
