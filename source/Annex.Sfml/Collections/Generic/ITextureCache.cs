using SFML.Graphics;

namespace Annex.Sfml.Collections.Generic
{
    internal interface ITextureCache
    {
        Texture GetTexture(string textureId);
    }
}
