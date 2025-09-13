using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts;

public enum Updatability
{
    NeverUpdates,
    Updates
}

public class BatchTextureContext : DrawContext
{
    public string TextureId { get; }
    public (float x, float y)[] Positions { get; }
    public (float x, float y)[]? RenderSizes { get; init; }
    public (float x, float y)[]? RenderOffsets { get; init; }
    public (int top, int left, int width, int height)[]? SourceTextureRects { get; init; }
    public RGBA[]? RenderColors { get; init; }
    public float[]? Rotations { get; init; } // TODO: Not supported, currently broken

    public readonly Updatability UpdateFrequency;

    public BatchTextureContext(string textureId, (float x, float y)[] positions, Updatability updateFrequency)
    {
        TextureId = textureId;
        Positions = positions;
        UpdateFrequency = updateFrequency;

        RenderSizes = null;
        RenderOffsets = null;
        SourceTextureRects = null;
        RenderColors = null;
        Rotations = null;
    }

    public (float x, float y)? GetSize(int index)
    {
        if (RenderSizes == null)
            return null;

        if (RenderSizes.Length == 1)
        {
            index = 0;
        }

        return RenderSizes[index];
    }

    public (float x, float y) GetPosition(int index)
    {
        return Positions[index];
    }

    public (float x, float y)? GetOffset(int index)
    {
        if (RenderOffsets == null)
            return null;

        if (RenderOffsets.Length == 1)
        {
            index = 0;
        }

        return RenderOffsets[index];
    }

    public (int top, int left, int width, int height)? GetSourceTextureRect(int index)
    {
        if (SourceTextureRects == null)
            return null;

        if (SourceTextureRects.Length == 1)
        {
            index = 0;
        }

        return SourceTextureRects[index];
    }

    public RGBA? GetColor(int index)
    {
        if (RenderColors == null)
            return null;

        if (RenderColors.Length == 1)
        {
            index = 0;
        }

        return RenderColors[index];
    }

    public float? GetRotation(int index)
    {
        if (Rotations == null)
            return null;

        if (Rotations.Length == 1)
        {
            index = 0;
        }

        return Rotations[index];
    }
}
