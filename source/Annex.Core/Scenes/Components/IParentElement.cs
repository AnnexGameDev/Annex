namespace Annex.Core.Scenes.Components
{
    public interface IParentElement : IUIElement
    {
        IEnumerable<IUIElement> Children { get; }
        void AddChild(IUIElement child);
    }
}
