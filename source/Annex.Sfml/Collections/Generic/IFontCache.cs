using SFML.Graphics;

namespace Annex.Sfml.Collections.Generic
{
    internal interface IFontCache
    {
        Font GetFont(string fontId);
    }
}
