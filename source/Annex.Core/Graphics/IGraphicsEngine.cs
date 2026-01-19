using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Graphics;

public interface IGraphicsEngine
{
    IWindow CreateWindow(string title, uint width, uint height, WindowStyle windowStyle);

    FloatRect GetTextBounds(TextContext textContext, bool forceContextUpdate = true);
    float GetCharacterX(TextContext textContext, int index, bool forceContextUpdate = true);
}