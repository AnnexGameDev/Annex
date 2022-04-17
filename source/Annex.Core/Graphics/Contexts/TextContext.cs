using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts
{
    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right,
    }

    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom,
    }

    public class TextContext : DrawContext
    {
        public Shared<string> Text { get; }
        public Shared<string> Font { get; }

        public IVector2<float>? Position { get; init; }
        public IVector2<float>? PositionOffset { get; init; }

        public Shared<uint>? FontSize { get; set; }
        public RGBA? Color { get; init; }

        public Shared<float>? BorderThickness { get; init; }
        public RGBA? BorderColor { get; init; }

        public Shared<float>? Rotation { get; init; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }

        public TextContext(Shared<string> text, Shared<string> font) {
            this.Text = text;
            this.Font = font;

            this.Position = null;
            this.PositionOffset = null;
            this.FontSize = null;
            this.Color = null;
            this.BorderThickness = null;
            this.BorderColor = null;
            this.Rotation = null;

            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }
}
