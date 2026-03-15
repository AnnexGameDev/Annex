namespace Annex.Core.Scenes;

internal class UIElementNotFoundException : Exception
{
    public UIElementNotFoundException(string? elementId) : base($"Unable to find the UI element {elementId}")
    {
    }
}
