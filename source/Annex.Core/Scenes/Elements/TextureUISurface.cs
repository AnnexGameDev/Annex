using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Scenes.Elements;

public class TextureUISurface : IUISurface
{
    private readonly TextureContext _textureContext;

    public string Texture
    {
        get => _textureContext.TextureId;
        set => _textureContext.TextureId = value;
    }

    public TextureUISurface(string textureId, IUIElement parent)
    {
        _textureContext = new TextureContext(textureId, parent.Position)
        {
            RenderSize = parent.Size,
            Camera = KnownCamera.UI
        };
    }

    public void Dispose()
    {
        _textureContext.Dispose();
    }

    public void DrawOn(IWindow window, long timeDelta)
    {
        window.Draw(_textureContext);
    }

    public IUISurface CopyFor(IUIElement parent) => new TextureUISurface(Texture, parent);

    public void Tint(byte r, byte g, byte b)
    {
        _textureContext.RenderColor.Set(r, g, b);
    }
}
