using Annex.Core.Data;

namespace Annex.Core.Scenes.Components
{
    public class TextBox : LabeledTextureUIElement
    {
        public TextBox(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
        }
    }
}
