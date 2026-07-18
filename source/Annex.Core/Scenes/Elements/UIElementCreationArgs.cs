using Annex.Core.Data;

namespace Annex.Core.Scenes.Elements;

public readonly struct UIElementCreationArgs(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null)
{
    public readonly string? ElementId = elementId;
    public readonly IVector2<float>? Position = position;
    public readonly IVector2<float>? Size = size;
}
