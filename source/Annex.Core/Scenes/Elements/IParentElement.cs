using System.Runtime.CompilerServices;

namespace Annex.Core.Scenes.Elements;

public interface IParentElement : IUIElement
{
    IEnumerable<IUIElement> Children { get; }

    T GetElement<T>([CallerMemberName] string elementId = "") where T : class, IUIElement;
    T? GetElementById<T>(string id) where T : class, IUIElement;
    IUIElement? GetElementById(string id);
    IUIElement? GetFirstVisibleElement(float x, float y);
}
