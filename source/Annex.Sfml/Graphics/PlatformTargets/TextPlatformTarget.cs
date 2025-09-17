using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using Vector2f = SFML.System.Vector2f;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TextPlatformTarget : PlatformTarget
{
    private readonly Text _text;
    private readonly TextContext _textContext;
    private readonly FontAssetProvider _fontAssetProvider;

    private float _superSampleScale;
    private RenderTexture? _renderedText_Texture;
    private Sprite? _renderedText_Sprite;

    public override object Target => _text;

    public TextPlatformTarget(TextContext textContext, FontAssetProvider fontAssetProvider)
    {
        _textContext = textContext;
        _fontAssetProvider = fontAssetProvider;
        _text = new();
    }

    public override void Dispose()
    {
        _text.Dispose();
        _renderedText_Sprite?.Dispose();
        _renderedText_Texture?.Dispose();
        // _textContext isn't owned by us
    }

    protected override void Draw(RenderTarget renderTarget)
    {
        if (UpdateTextTextureIfNeeded())
        {
            ReCreateTextTexture();
        }
        UpdateTexturePositionIfNeeded();
        renderTarget.Draw(_renderedText_Sprite);
    }

    private void UpdateTexturePositionIfNeeded()
    {
        UpdatePosition(_textContext.Position);
        UpdateOrigin(_textContext.HorizontalAlignment, _textContext.VerticalAlignment, _textContext.PositionOffset);
        UpdateRotation(_textContext.Rotation);
    }

    private void ReCreateTextTexture()
    {
        uint originalCharacterSize = _text.CharacterSize;
        _text.CharacterSize = (uint)(_text.CharacterSize * _superSampleScale); // temporarily scale the text size for rendering purposes

        _renderedText_Sprite?.Dispose();
        _renderedText_Texture?.Dispose();

        var bounds = _text.GetLocalBounds();
        _text.Position = new Vector2f(-bounds.Left, -bounds.Top);

        uint width = (uint)Math.Ceiling(bounds.Width + 2);
        uint height = (uint)Math.Ceiling(bounds.Height + 2);

        _renderedText_Texture = new RenderTexture(width, height);
        _renderedText_Texture.Clear(Color.Transparent);
        _renderedText_Texture.Draw(_text);
        _renderedText_Texture.Display();

        _text.CharacterSize = originalCharacterSize; // undo

        _renderedText_Sprite = new Sprite(_renderedText_Texture.Texture)
        {
            Scale = new Vector2f(1.0f / _superSampleScale, 1.0f / _superSampleScale)
        };
    }

    private bool UpdateTextTextureIfNeeded()
    {
        bool update = false;
        update |= UpdateSuperSampleScale(_textContext.SuperSampleCount?.Value ?? 1);
        update |= UpdateFont(_textContext.Font.Value);
        update |= UpdateText(_textContext.Text.Value);
        update |= UpdateFontSize(_textContext.FontSize);
        update |= UpdateFontColor(_textContext.Color);
        update |= UpdateBorderThickness(_textContext.BorderThickness);
        update |= UpdateBorderColor(_textContext.BorderColor);
        return update;
    }

    private bool UpdateSuperSampleScale(float value)
    {
        if (_superSampleScale != value)
        {
            _superSampleScale = value;
            return true;
        }
        return false;
    }

    private void UpdateRotation(IShared<float>? rotation)
    {
        const float DefaultRotation = 0;
        var finalRotation = rotation?.Value ?? DefaultRotation;
        if (_renderedText_Sprite.Rotation != finalRotation)
        {
            _renderedText_Sprite.Rotation = finalRotation;
        }
    }

    private void UpdateOrigin(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, IVector2<float>? positionOffset)
    {
        var bounds = _renderedText_Sprite.GetLocalBounds();
        Vector2f desiredOrigin = new Vector2f(
            horizontalAlignment.Align(_renderedText_Texture.Size.X),
            verticalAlignment.Align(_renderedText_Texture.Size.Y)
        );
        desiredOrigin.X -= (positionOffset?.X ?? 0) * _superSampleScale;
        desiredOrigin.Y -= (positionOffset?.Y ?? 0) * _superSampleScale;

        if (_renderedText_Sprite.Origin != desiredOrigin)
        {
            _renderedText_Sprite.Origin = desiredOrigin;
        }
    }

    private void UpdatePosition(IVector2<float>? position)
    {
        if (_renderedText_Sprite.Position.DoesNotEqual(position))
        {
            _renderedText_Sprite.Position = position.ToSFML();
        }
    }

    private bool UpdateBorderColor(RGBA? borderColor)
    {
        if (_text.OutlineColor.DoesNotEqual(borderColor, Color.Black))
        {
            _text.OutlineColor = borderColor.ToSFML(KnownColor.Black);
            return true;
        }
        return false;
    }

    private bool UpdateBorderThickness(IShared<float>? borderThickness)
    {
        const float DefaultBorderThickness = 0;
        float finalBorderThickness = borderThickness?.Value ?? DefaultBorderThickness;
        if (_text.OutlineThickness != finalBorderThickness)
        {
            _text.OutlineThickness = finalBorderThickness;
            return true;
        }
        return false;
    }

    private bool UpdateFontColor(RGBA? color)
    {
        if (_text.FillColor.DoesNotEqual(color, Color.Black))
        {
            _text.FillColor = color.ToSFML(KnownColor.Black);
            return true;
        }
        return false;
    }

    private bool UpdateFontSize(IShared<uint>? fontSize)
    {
        const uint DefaultFontSize = 12;
        uint finalFontSize = fontSize?.Value ?? DefaultFontSize;
        if (_text.CharacterSize != finalFontSize)
        {
            _text.CharacterSize = finalFontSize;
            return true;
        }
        return false;
    }

    private bool UpdateText(string text)
    {
        if (_text.DisplayedString != text)
        {
            _text.DisplayedString = text;
            return true;
        }
        return false;
    }

    private bool UpdateFont(string font)
    {
        if (!_fontAssetProvider.TryGetAsset(font, out var sfmlFont))
        {
            throw new KeyNotFoundException(font);
        }
        if (sfmlFont != _text.Font)
        {
            _text.Font = sfmlFont;
            return true;
        }
        return false;
    }

    public Core.Data.FloatRect GetTextBounds()
    {
        return _text.GetLocalBounds().ToAnnex();
    }

    public float GetCharacterX(int index)
    {
        if (index == _text.DisplayedString.Length)
        {
            return GetTextBounds().Width;
        }
        return _text.FindCharacterPos((uint)index).X;
    }
}
