using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts
{
    public class SpritesheetContext : DrawContext
    {
        public IShared<string> TextureId { get; }
        public IVector2<float> Position { get; }
        public RGBA? RenderColor { get; init; }
        public IShared<float>? Rotation { get; init; }
        public IVector2<float>? RenderOffset { get; init; }
        public IVector2<float>? RenderSize { get; init; }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public int NumRows { get; }
        public int NumColumns { get; }

        public SpritesheetContext(IShared<string> textureId, IVector2<float> position, uint numRows, uint numColumns) {
            this.TextureId = textureId;
            this.Position = position;
            this.RenderColor = null;
            this.Rotation = null;
            this.RenderOffset = null;
            this.RenderSize = null;

            this.NumRows = (int)numRows;
            this.NumColumns = (int)numColumns;
        }

        public void StepRow() {
            this.SetRow(this.Row + 1);
        }

        public void StepColumn() {
            this.SetColumn(this.Column + 1);
        }

        public void SetRow(int row) {
            this.Row = row % this.NumRows;
        }

        public void SetColumn(int column) {
            this.Column = column % this.NumColumns;
        }
    }
}