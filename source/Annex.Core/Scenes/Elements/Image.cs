using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class Image : UIElement, IImage
{
    protected readonly TextureContext BackgroundContext;
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

    public Image(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
        this.BackgroundContext = new TextureContext(string.Empty.ToShared(), this.Position)
        {
            RenderSize = this.Size,
            Camera = CameraId.UI.ToString()
        };
    }

    protected override void DrawInternal(ICanvas canvas) {

        string textureToRender = BackgroundTextureId;

        if (_hasMouse && HoverBackgroundTextureId is not null)
        {
            textureToRender = HoverBackgroundTextureId;
        } else if (IsFocused && FocusedBackgroundTextureId is not null)
        {
            textureToRender = FocusedBackgroundTextureId;
        }

        BackgroundContext.TextureId.Set(textureToRender);
        canvas.Draw(this.BackgroundContext);
    }

    public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent) {
        base.OnMouseMoved(mouseMovedEvent);
        _hasMouse = true;
    }

    public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent) {
        base.OnMouseLeft(mouseMovedEvent);
        _hasMouse = false;
    }
}
