using Annex.Data.Binding;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace SampleProject.Data.Map
{
    public class BasicMap : IDrawableObject
    {
        private SurfaceContext _backgroundSprite;
        private PString _backgroundSurface;

        public BasicMap() {
            this._backgroundSurface = new PString("gui/backgrounds/main.png");
            this._backgroundSprite = new SurfaceContext(this._backgroundSurface) {
                RenderSize = new PVector(2000, 2000)
            };
        }

        public void Draw(IDrawableContext surfaceContext) {
            surfaceContext.Draw(this._backgroundSprite);
        }
    }
}
