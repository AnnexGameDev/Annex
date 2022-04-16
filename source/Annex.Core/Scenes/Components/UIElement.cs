﻿using Annex.Core.Data;
using Annex.Core.Graphics;

namespace Annex.Core.Scenes.Components
{
    public abstract class UIElement : IUIElement
    {
        public string ElementID { get; set; }
        public IVector2<float> Size { get; }
        public IVector2<float> Position { get; }
        public bool Visible { get; set; }

        public UIElement(string elementId, IVector2<float>? position = null, IVector2<float>? size = null) {
            this.ElementID = elementId;
            this.Position = position ?? new Vector2f();
            this.Size = size ?? new Vector2f();
            this.Visible = true;
        }
        private bool disposedValue = false;

        public void Draw(ICanvas canvas) {
            if (this.Visible)
                this.DrawInternal(canvas);
        }

        protected abstract void DrawInternal(ICanvas canvas);

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UIElement()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}