using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class Image : UIElement, IImage
{
    public TextureContext TextureContext { get; }
    private bool _hasMouse;

    public string? HoverBackgroundTextureId
    {
        get;
        set;
    }

    public string? FocusedBackgroundTextureId
    {
        get;
        set;
    }

    public string BackgroundTextureId
    {
        get;
        set;
    }

    public Image(UIElementCreationArgs? args) : base(args)
    {
        TextureContext = new TextureContext(string.Empty, Position)
        {
            RenderSize = Size,
            Camera = KnownCamera.UI
        };
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        string textureToRender = BackgroundTextureId;

        if (_hasMouse && HoverBackgroundTextureId is not null)
        {
            textureToRender = HoverBackgroundTextureId;
        }
        else if (IsFocused && FocusedBackgroundTextureId is not null)
        {
            textureToRender = FocusedBackgroundTextureId;
        }

        TextureContext.TextureId = textureToRender;
        window.Draw(TextureContext);
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
