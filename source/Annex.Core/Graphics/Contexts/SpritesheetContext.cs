using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts;

public class SpritesheetContext : DrawContext
{
    public string TextureId { get; set; }
    public IReadonlyVector2<float> Position { get; }
    public RGBA? RenderColor { get; init; }
    public float? Rotation { get; set; }
    public IReadonlyVector2<float>? RenderOffset { get; init; }
    public IReadonlyVector2<float>? RenderSize { get; init; }

    public Vector2f FrameSize { get; } = new Vector2f();

    public int Row { get; private set; }
    public int Column { get; private set; }
    public int NumRows { get; }
    public int NumColumns { get; }

    public SpritesheetContext(string textureId, IReadonlyVector2<float> position, int numRows, int numColumns) {
        TextureId = textureId;
        Position = position;
        RenderColor = null;
        Rotation = null;
        RenderOffset = null;
        RenderSize = null;

        NumRows = numRows;
        NumColumns = numColumns;
    }

    public void StepRow() {
        SetRow(Row + 1);
    }

    public void StepColumn() {
        SetColumn(Column + 1);
    }

    public void SetRow(int row) {
        Row = row % NumRows;
    }

    public void SetColumn(int column) {
        Column = column % NumColumns;
    }
}