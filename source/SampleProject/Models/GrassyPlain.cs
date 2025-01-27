using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;

namespace SampleProject.Models
{
    public sealed class GrassyPlain : IDrawable
    {
        private readonly TextureContext _plainTexture;

        public GrassyPlain()
        {
            this._plainTexture = new TextureContext("plain.png".ToShared())
            {
            };
        }

        public void Dispose()
        {
            this._plainTexture.Dispose();
        }

        public void DrawOn(IWindow window)
        {
            window.Draw(this._plainTexture);
        }
    }
}