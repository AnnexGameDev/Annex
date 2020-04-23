#nullable enable
using Annex.Data;
using Annex.Data.Shared;

namespace Annex.Graphics.Contexts
{
    public class SpriteSheetContext : DrawingContext
    {
        public String SourceTextureName {
            get {
                return this._internalTexture.SourceTextureName;
            }
            set {
                this._internalTexture.SourceTextureName = value;
            }
        }
        public Vector RenderPosition {
            get {
                return this._internalTexture.RenderPosition;
            }
            set {
                this._internalTexture.RenderPosition = value;
            }
        }
        public Vector? RenderSize {
            get {
                return this._internalTexture.RenderSize;
            }
            set {
                this._internalTexture.RenderSize = value;
            }
        }
        public RGBA? RenderColor {
            get {
                return this._internalTexture.RenderColor;
            }
            set {
                this._internalTexture.RenderColor = value;
            }
        }
        public float Rotation {
            get {
                return this._internalTexture.Rotation;
            }
            set {
                this._internalTexture.Rotation = value;
            }
        }
        public Vector RelativeRotationOrigin {
            get {
                return this._internalTexture.RelativeRotationOrigin;
            }
            set {
                this._internalTexture.RelativeRotationOrigin = value;
            }
        }
        internal IntRect? SourceTextureRect {
            get {
                return this._internalTexture.SourceTextureRect;
            }
            set {
                this._internalTexture.SourceTextureRect = value;
            }
        }

        internal TextureContext _internalTexture;

        public readonly Int Row;
        public readonly Int Column;
        public readonly int NumRows;
        public readonly int NumColumns;

        public SpriteSheetContext(String textureName, uint numRows, uint numColumns) {
            this._internalTexture = new TextureContext(textureName);
            this.Row = new Int();
            this.Column = new Int();
            this.NumColumns = (int)numColumns;
            this.NumRows = (int)numRows;
            this.SourceTextureRect = null;
        }

        public void StepRow() {
            this.SetRow(this.Row.Value + 1);
        }

        public void StepColumn() {
            this.SetColumn(this.Column.Value + 1);
        }

        public void SetRow(int row) {
            this.Row.Value = row % this.NumRows;
        }

        public void SetColumn(int column) {
            this.Column.Value = column % this.NumColumns;
        }
    }
}
