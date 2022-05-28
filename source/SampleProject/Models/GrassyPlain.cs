﻿using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Graphics.Contexts;

namespace SampleProject.Models
{
    public sealed class GrassyPlain : IDrawable
    {
        private readonly TextureContext _plainTexture;

        public GrassyPlain() {
            this._plainTexture = new TextureContext("plain.png") {
            };
        }

        public void Dispose() {
            this._plainTexture.Dispose();
        }

        public void Draw(ICanvas canvas) {
            canvas.Draw(this._plainTexture);
        }
    }
}