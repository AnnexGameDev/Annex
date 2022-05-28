using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics.Contexts;
using Annex_Old.Core.Graphics.Windows;

namespace Annex_Old.Core.Graphics
{
    public interface IGraphicsEngine
    {
        IWindow CreateWindow();

        FloatRect GetTextBounds(TextContext textContext, bool forceContextUpdate = true);
        float GetCharacterX(TextContext textContext, int index, bool forceContextUpdate = true);
    }
}