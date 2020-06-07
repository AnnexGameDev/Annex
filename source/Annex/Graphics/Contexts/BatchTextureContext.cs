using Annex.Data;
using SFML.Graphics;
using static Annex.Graphics.Errors;

namespace Annex.Graphics.Contexts
{
    public class BatchTextureContext : DrawingContext
    {
        public string SourceTextureName { get; set; }
        internal (float x, float y)[] RenderPositions { get; set; }
        internal (float x, float y)[] RenderSizes { get; set; }
        internal (int top, int left, int width, int height)[]? SourceTextureRects { get; set; }
        internal RGBA[]? RenderColors { get; set; }

        internal object? vertex_cache { get; set; }

        public BatchTextureContext(string textureName, (float x, float y)[] renderPositions, (float x, float y) renderSizes, (int top, int left, int width, int height)? rect = null, RGBA? color = null) {
            this.SourceTextureName = textureName;
            this.RenderPositions = renderPositions;

            this.RenderSizes = new (float, float)[renderPositions.Length];
            for (int i = 0; i < RenderPositions.Length; i++) {
                this.RenderSizes[i] = renderSizes;
            }

            if (rect != null) {
                var non_null_rect = ((int, int, int, int))rect;
                this.SourceTextureRects = new (int, int, int, int)[RenderPositions.Length];
                for (int i = 0; i < RenderPositions.Length; i++) {
                    this.SourceTextureRects[i] = non_null_rect;
                }
            }

            if (color != null) {
                this.RenderColors = new RGBA[RenderPositions.Length];
                for (int i = 0; i < RenderPositions.Length; i++) {
                    this.RenderColors[i] = color;
                }
            }
        }

        public BatchTextureContext(string textureName, (float x, float y)[] renderPositions, (float x, float y)[] renderSizes, (int top, int left, int width, int height)[]? sourceTextureRects, RGBA[]? colors = null) {
            this.RenderPositions = renderPositions;
            int batchSize = this.RenderPositions.Length;

            Debug.ErrorIf(renderSizes.Length != batchSize, STR_INCOMPATIBLE_BATCH_SIZE.Format(nameof(BatchTextureContext), batchSize, renderSizes.Length, nameof(renderSizes)));
            Debug.ErrorIf((sourceTextureRects?.Length ?? batchSize) != batchSize, STR_INCOMPATIBLE_BATCH_SIZE.Format(nameof(BatchTextureContext), batchSize, sourceTextureRects?.Length, nameof(sourceTextureRects)));
            Debug.ErrorIf((colors?.Length ?? batchSize) != batchSize, STR_INCOMPATIBLE_BATCH_SIZE.Format(nameof(BatchTextureContext), batchSize, colors?.Length, nameof(colors)));

            this.SourceTextureRects = sourceTextureRects;
            this.SourceTextureName = textureName;
            this.RenderSizes = renderSizes;
            this.RenderColors = colors;
        }
    }
}
