using Annex.Core.Data;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using IntRect = SFML.Graphics.IntRect;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal abstract class SpritePlatformTarget : TransformablePlatformTarget
{
    private readonly Sprite _sprite;
    protected override Transformable Transformable => _sprite;
    private readonly ITextureCache _textureCache;

    public SpritePlatformTarget(ITextureCache textureCache)
    {
        _textureCache = textureCache;
        _sprite = new();
    }

    public override void Dispose()
    {
        _sprite.Dispose();
    }

    protected override void Draw(RenderTarget renderTarget)
    {
        UpdateIfNeeded();
        renderTarget.Draw(_sprite);
    }

    protected abstract void UpdateIfNeeded();

    protected Texture UpdateTexture(string textureId)
    {
        var texture = _textureCache.GetTexture(textureId);
        if (texture != _sprite.Texture)
        {
            _sprite.Texture = texture;
        }
        return _sprite.Texture;
    }

    protected IntRect UpdateTextureRect(Core.Data.IntRect? sourceTextureRect)
    {
        if (_sprite.TextureRect.DoesNotEqual(sourceTextureRect, 0, 0, (int)_sprite.Texture.Size.X, (int)_sprite.Texture.Size.Y))
        {
            _sprite.TextureRect = sourceTextureRect.ToSFML();
        }
        return _sprite.TextureRect;
    }

    protected IntRect UpdateTextureRect(int top, int left, int width, int height)
    {
        if (_sprite.TextureRect.DoesNotEqual(null, top, left, width, height))
        {
            _sprite.TextureRect = new IntRect(left, top, width, height);
        }
        return _sprite.TextureRect;
    }

    protected Color UpdateColor(RGBA? color)
    {
        if (_sprite.Color.DoesNotEqual(color, Color.White))
        {
            _sprite.Color = color.ToSFML(KnownColor.White);
        }
        return _sprite.Color;
    }
}
