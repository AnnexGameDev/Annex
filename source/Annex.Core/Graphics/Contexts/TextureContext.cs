using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts;

public class TextureContext : DrawContext
{
    public string TextureId { get; set; }
    public IVector2<float> Position { get; }
    public IVector2<float>? RenderOffset { get; init; }
    public IVector2<float>? RenderSize { get; init; }
    public IntRect? SourceTextureRect { get; init; }
    public RGBA RenderColor { get; init; }
    public float? Rotation { get; init; }

    public TextureContext(string textureId) : this(textureId, new Vector2f())
    {

    }

    public TextureContext(string textureId, IVector2<float> position)
    {
        TextureId = textureId;
        Position = position;
        RenderOffset = null;
        RenderSize = null;
        SourceTextureRect = null;
        RenderColor = KnownColor.White;
        Rotation = null;
    }
}