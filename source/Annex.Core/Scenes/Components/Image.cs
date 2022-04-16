using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Scenes.Components
{
    public class Image : UIElement
    {
        protected readonly TextureContext BackgroundContext;
        public string BackgroundTextureId { 
            get => BackgroundContext.TextureId.Value;
            set => BackgroundContext.TextureId.Value = value;
        }

        public Image(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
            this.BackgroundContext = new TextureContext(string.Empty, this.Position) {
                RenderSize = this.Size,
                Camera = CameraId.UI.ToString()
            };
        }

        protected override void DrawInternal(ICanvas canvas) {
            canvas.Draw(this.BackgroundContext);
        }
    }
}
