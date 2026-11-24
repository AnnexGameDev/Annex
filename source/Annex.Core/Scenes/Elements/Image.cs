using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class Image : UIElement, IImage
{
    private bool _hasMouse;

    public IUISurface? HoverSurface
    {
        get;
        set;
    }

    public IUISurface? FocusSurface
    {
        get;
        set;
    }

    public IUISurface BackgroundSurface
    {
        get;
        set;
    }

    public Image(UIElementCreationArgs? args) : base(args)
    {
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        var surfaceToRender = BackgroundSurface;

        if (_hasMouse && HoverSurface is not null)
        {
            surfaceToRender = HoverSurface;
        }
        else if (IsFocused && FocusSurface is not null)
        {
            surfaceToRender = FocusSurface;
        }

        surfaceToRender?.DrawOn(window, timeDelta);
    }

    public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseMoved(mouseMovedEvent);
        _hasMouse = true;
    }

    public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseLeft(mouseMovedEvent);
        _hasMouse = false;
    }
}
