using Annex.Data;

namespace Annex.Graphics.Contexts
{
    public class SurfaceContext
    {
        public PString SourceSurfaceName { get; set; }
        public Vector2f RenderPosition { get; set; }
        public Vector2f RenderSize { get; set; }
        public IntRect? SourceSurfaceRect { get; set; }
        public RGBA? RenderColor { get; set; }
        public float Rotation { get; set; }
        public Vector2f RelativeRotationOrigin { get; set; }
        public bool IsAbsolute { get; set; }

        public SurfaceContext(PString surfaceName) {
            this.SourceSurfaceName = surfaceName;
            this.RenderPosition = new Vector2f();
            this.RenderSize = new Vector2f();
            this.SourceSurfaceRect = null;
            this.Rotation = 0;
            this.IsAbsolute = true;
            this.RenderColor = null;
            this.RelativeRotationOrigin = new Vector2f();
        }
    }
}
