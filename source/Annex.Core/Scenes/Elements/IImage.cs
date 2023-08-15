namespace Annex.Core.Scenes.Elements;

public interface IImage : IUIElement
{
    string? HoverBackgroundTextureId { get; set; }
    string BackgroundTextureId { get; set; }
}
