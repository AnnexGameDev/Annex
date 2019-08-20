using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace SampleProject.Data.Map
{
    public class BasicMap : IDrawableObject
    {
        private SurfaceContext _backgroundSprite;
        private String _backgroundSurface;

        public BasicMap() {
            this._backgroundSurface = new String("gui/backgrounds/main.png");
            this._backgroundSprite = new SurfaceContext(this._backgroundSurface) {
                RenderSize = new Vector(2000, 2000)
            };
        }

        public void Draw(IDrawableContext surfaceContext) {
            surfaceContext.Draw(this._backgroundSprite);
        }
    }
}
