using Annex.Graphics;

namespace Annex.UserInterface.Components
{
    public abstract class UIElement : IDrawableObject
    {
        public abstract void Draw(IDrawableContext surfaceContext);
    }
}
