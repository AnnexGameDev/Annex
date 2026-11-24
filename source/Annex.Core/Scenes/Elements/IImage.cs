namespace Annex.Core.Scenes.Elements;

public interface IImage : IUIElement
{
    public IUISurface? BackgroundSurface { get; set; }

    public IUISurface? FocusSurface { get; set; }

    public IUISurface? HoverSurface { get; set; }
}
