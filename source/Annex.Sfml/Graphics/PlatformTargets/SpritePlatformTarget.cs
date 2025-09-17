using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using IntRect = SFML.Graphics.IntRect;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal abstract class SpritePlatformTarget<T> : TransformablePlatformTarget where T : DrawContext
{
    private readonly Sprite _sprite;
    protected override Transformable Transformable => _sprite;
    public override object Target => _sprite.Texture;
    private readonly TextureAssetProvider _textureAssetProvider;
    protected readonly T Context;
    private RenderStates _renderState = RenderStates.Default;

    public SpritePlatformTarget(T context, TextureAssetProvider textureAssetProvider)
    {
        Context = context;
        _textureAssetProvider = textureAssetProvider;
        _sprite = new();
    }

    public override void Dispose()
    {
        _sprite.Dispose();
    }

    protected override void Draw(RenderTarget renderTarget)
    {
        UpdateIfNeeded();
        _renderState.Shader = ShaderCache.GetShader(Context.Shader);
        renderTarget.Draw(_sprite, _renderState);
    }

    protected abstract void UpdateIfNeeded();

    protected Texture UpdateTexture(string textureId)
    {
        if (!_textureAssetProvider.TryGetAsset(textureId, out var texture))
        {
            throw new KeyNotFoundException(textureId);
        }
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
