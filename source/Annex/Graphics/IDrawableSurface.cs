﻿using Annex.Graphics.Contexts;

namespace Annex.Graphics
{
    public interface IDrawableSurface
    {
        void Draw(BatchTextureContext ctx);
        void Draw(TextContext ctx);
        void Draw(TextureContext ctx);
        void Draw(SpriteSheetContext sheet);
        void Draw(SolidRectangleContext rectangle);
    }
}
