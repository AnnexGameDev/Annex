﻿using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Graphics.Contexts;

namespace Annex_Old.Core.Scenes.Components
{
    public class Image : UIElement, IImage
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
