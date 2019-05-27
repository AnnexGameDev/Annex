namespace Annex.Data
{
    public class OffsetVector2f : Vector2f
    {
        private readonly Vector2f _original;
        private readonly Vector2f _offset;

        public override float X {
            get {
                return this._offset.X + this._original.X;
            }
            set {
                this._offset.X = value;
            }
        }
        public override float Y {
            get {
                return this._offset.Y + this._original.Y;
            }
            set {
                this._offset.Y = value;
            }
        }

        public OffsetVector2f(Vector2f original, Vector2f offset) {
            this._original = original;
            this._offset = offset;
        }
    }
}
