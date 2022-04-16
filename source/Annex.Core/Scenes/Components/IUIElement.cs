using Annex.Core.Data;
using Annex.Core.Graphics;

namespace Annex.Core.Scenes.Components
{
    public interface IUIElement : IDrawable
    {
        string ElementID { get; set; }
        IVector2<float> Size { get; }
        IVector2<float> Position { get; }
        bool Visible { get; set; }
    }
}
