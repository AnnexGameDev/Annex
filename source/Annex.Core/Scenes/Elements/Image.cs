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

    public Image(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size)
    {
        TextureContext = new TextureContext(string.Empty.ToShared(), Position)
        {
            RenderSize = Size,
            Camera = CameraId.UI.ToString()
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

        TextureContext.TextureId.Set(textureToRender);
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
