using Annex.Core.Data;

namespace Annex.Core.Graphics.Windows
{
    public interface IWindow
    {
        Vector2ui Size { get; set;  }
        Vector2i Position { get; set; }
        string Title { get; set; }

        bool IsVisible { get; set; }
    }
}