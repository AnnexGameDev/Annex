using Annex.Core.Graphics;

namespace Annex.Core.Scenes.Elements;

public interface IUISurface : IDrawable
{
    IUISurface CopyFor(IUIElement parent);
}