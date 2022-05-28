namespace Annex_Old.Core.Scenes.Components
{
    public interface IParentElement : IUIElement
    {
        IEnumerable<IUIElement> Children { get; }
        void AddChild(IUIElement child);
        IUIElement? GetElementById(string id);
        IUIElement? GetFirstVisibleElement(float x, float y);
    }
}
