using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class SolidCirclePlatformTarget : TransformablePlatformTarget
{
    private readonly SolidCircleContext _circleContext;
    private readonly CircleShape _circle;
    protected override Transformable Transformable => _circle;
    public override object Target => _circle;

    public SolidCirclePlatformTarget(SolidCircleContext drawContext)
    {
        _circleContext = drawContext;
        _circle = new CircleShape();
    }

    protected override void Draw(RenderTarget renderTarget)
    {
        UpdateIfNeeded();
        renderTarget.Draw(_circle);
    }

    private void UpdateIfNeeded()
    {
        if (_circle.FillColor.DoesNotEqual(_circleContext.FillColor))
        {
            _circle.FillColor = _circleContext.FillColor.ToSFML();
        }
        if (_circle.Radius != _circleContext.Radius)
        {
            _circle.Radius = _circleContext.Radius;
        }

        (var position, var origin) = UpdatePositionAndOrigin(_circleContext.Position, _circleContext.RenderOffset);

        if (_circle.OutlineColor.DoesNotEqual(_circleContext.BorderColor, Color.Transparent))
        {
            _circle.OutlineColor = _circleContext.BorderColor.ToSFML(KnownColor.Transparent);
        }

        const float defaultThickness = 0;
        if (_circle.OutlineThickness != (_circleContext.BorderThickness ?? defaultThickness))
        {
            _circle.OutlineThickness = _circleContext.BorderThickness ?? defaultThickness;
        }
    }

    public override void Dispose()
    {
        _circle.Dispose();
    }
}
