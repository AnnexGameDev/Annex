using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class SolidRectanglePlatformTarget : TransformablePlatformTarget
{
    private readonly SolidRectangleContext _rectangleContext;
    private readonly RectangleShape _rectangle;
    protected override Transformable Transformable => _rectangle;
    public override object Target => _rectangle;

    public SolidRectanglePlatformTarget(SolidRectangleContext drawContext)
    {
        _rectangleContext = drawContext;
        _rectangle = new RectangleShape();
    }

    protected override void Draw(RenderTarget renderTarget)
    {
        UpdateIfNeeded();
        renderTarget.Draw(_rectangle);
    }

    private void UpdateIfNeeded()
    {
        if (_rectangle.FillColor.DoesNotEqual(_rectangleContext.FillColor))
        {
            _rectangle.FillColor = _rectangleContext.FillColor.ToSFML();
        }

        if (_rectangle.Size.DoesNotEqual(_rectangleContext.Size))
        {
            _rectangle.Size = _rectangleContext.Size.ToSFML();
        }

        (var position, var origin) = UpdatePositionAndOrigin(_rectangleContext.Position, _rectangleContext.RenderOffset);
        UpdateRotation(_rectangleContext.Rotation);


        if (_rectangle.OutlineColor.DoesNotEqual(_rectangleContext.BorderColor, Color.Transparent))
        {
            _rectangle.OutlineColor = _rectangleContext.BorderColor.ToSFML(KnownColor.Transparent);
        }

        const float defaultThickness = 0;
        if (_rectangle.OutlineThickness != (_rectangleContext.BorderThickness ?? defaultThickness))
        {
            _rectangle.OutlineThickness = (_rectangleContext.BorderThickness ?? defaultThickness);
        }
    }

    public override void Dispose()
    {
        _rectangle.Dispose();
        // _rectangleContext is not owned by us
    }
}
