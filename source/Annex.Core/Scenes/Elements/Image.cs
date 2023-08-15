using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class Image : UIElement, IImage
{
    protected readonly TextureContext BackgroundContext;

    private string? _renderBackgroundTextureId;

    public string? HoverBackgroundTextureId
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
        this.BackgroundContext = new TextureContext(string.Empty, this.Position)
        {
            RenderSize = this.Size,
            Camera = CameraId.UI.ToString()
        };
    }

    protected override void DrawInternal(ICanvas canvas) {
        BackgroundContext.TextureId.Value = _renderBackgroundTextureId ?? BackgroundTextureId;
        canvas.Draw(this.BackgroundContext);
    }

    public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent) {
        base.OnMouseMoved(mouseMovedEvent);
        _renderBackgroundTextureId = HoverBackgroundTextureId;
    }

    public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent) {
        base.OnMouseLeft(mouseMovedEvent);
        _renderBackgroundTextureId = BackgroundTextureId;
    }
}
