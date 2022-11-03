using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts
{
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

        public BatchTextureContext(string textureId, (float x, float y)[] positions, Updatability updateFrequency) {
            this.TextureId = textureId;
            this.Positions = positions;
            this.UpdateFrequency = updateFrequency;

            this.RenderSizes = null;
            this.RenderOffsets = null;
            this.SourceTextureRects = null;
            this.RenderColors = null;
            this.Rotations = null;
        }

        public (float x, float y)? GetSize(int index) {
            if (this.RenderSizes == null)
                return null;

            if (this.RenderSizes.Length == 1) {
                index = 0;
            }

            return this.RenderSizes[index];
        }

        public (float x, float y) GetPosition(int index) {
            return this.Positions[index];
        }

        public (float x, float y)? GetOffset(int index) {
            if (this.RenderOffsets == null)
                return null;

            if (this.RenderOffsets.Length == 1) {
                index = 0;
            }

            return this.RenderOffsets[index];
        }

        public (int top, int left, int width, int height)? GetSourceTextureRect(int index) {
            if (this.SourceTextureRects == null)
                return null;

            if (this.SourceTextureRects.Length == 1) {
                index = 0;
            }

            return this.SourceTextureRects[index];
        }

        public RGBA? GetColor(int index) {
            if (this.RenderColors == null)
                return null;

            if (this.RenderColors.Length == 1) {
                index = 0;
            }

            return this.RenderColors[index];
        }

        public float? GetRotation(int index) {
            if (this.Rotations == null)
                return null;

            if (this.Rotations.Length == 1) {
                index = 0;
            }

            return this.Rotations[index];
        }
    }
}
