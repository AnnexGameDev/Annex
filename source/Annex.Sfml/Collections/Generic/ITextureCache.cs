using SFML.Graphics;

namespace Annex_Old.Sfml.Collections.Generic
{
    internal interface ITextureCache
    {
        Texture GetTexture(string textureId);
    }
}
