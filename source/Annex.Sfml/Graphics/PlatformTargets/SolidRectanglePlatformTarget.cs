using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal class SolidRectanglePlatformTarget : TransformablePlatformTarget
    {
        private readonly SolidRectangleContext _rectangleContext;
        private readonly RectangleShape _rectangle;
        protected override Transformable Transformable => this._rectangle;

        public SolidRectanglePlatformTarget(SolidRectangleContext drawContext) {
            this._rectangleContext = drawContext;
            this._rectangle = new RectangleShape();
        }


        public override void Dispose() {
            this._rectangleContext.Dispose();
        }

        protected override void Draw(RenderTarget renderTarget) {
            this.UpdateIfNeeded();
            renderTarget.Draw(this._rectangle);
        }

        private void UpdateIfNeeded() {
            if (this._rectangle.FillColor.DoesNotEqual(this._rectangleContext.FillColor)) {
                this._rectangle.FillColor = this._rectangleContext.FillColor.ToSFML();
            }

            if (this._rectangle.Size.DoesNotEqual(this._rectangleContext.Size)) {
                this._rectangle.Size = this._rectangleContext.Size.ToSFML();
            }

            (var position, var origin) = UpdatePositionAndOrigin(this._rectangleContext.Position, this._rectangleContext.RenderOffset);
            UpdateRotation(this._rectangleContext.Rotation);

            if (this._rectangle.OutlineColor.DoesNotEqual(this._rectangleContext.BorderColor, Color.Transparent)) {
                this._rectangle.OutlineColor = this._rectangleContext.BorderColor.ToSFML(KnownColor.Transparent);
            }

            const float defaultThickness = 0;
            if (this._rectangle.OutlineThickness != (this._rectangleContext.BorderThickness?.Value ?? defaultThickness)) {
                this._rectangle.OutlineThickness = (this._rectangleContext.BorderThickness?.Value ?? defaultThickness);
            }
        }
    }
}
