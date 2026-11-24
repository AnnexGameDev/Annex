using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Scenes.Elements;

public class SolidRectangleUISurface : IUISurface
{
    private readonly SolidRectangleContext _solidRectangleContext;

    public RGBA FillColor
    {
        get => _solidRectangleContext.FillColor;
        set => _solidRectangleContext.FillColor.Set(value);
    }

    public RGBA BorderColor
    {
        get => _solidRectangleContext.BorderColor!;
        set => _solidRectangleContext.BorderColor!.Set(value);
    }

    public float BorderThickness
    {
        get => _solidRectangleContext.BorderThickness ?? 0;
        set => _solidRectangleContext.BorderThickness = value;
    }

    public SolidRectangleUISurface(RGBA color, IUIElement parent) : this(color, KnownColor.Transparent, 0, parent)
    {
    }

    public SolidRectangleUISurface(RGBA color, RGBA borderColor, float borderThickness, IUIElement parent)
    {
        _solidRectangleContext = new SolidRectangleContext(color, parent.Position, parent.Size)
        {
            Camera = KnownCamera.UI,
            BorderColor = borderColor,
            BorderThickness = borderThickness
        };
    }

    public void Dispose()
    {
        _solidRectangleContext.Dispose();
    }

    public void DrawOn(IWindow window, long timeDelta)
    {
        window.Draw(_solidRectangleContext);
    }

    public IUISurface CopyFor(IUIElement parent) => new SolidRectangleUISurface(FillColor, BorderColor, BorderThickness, parent);
}