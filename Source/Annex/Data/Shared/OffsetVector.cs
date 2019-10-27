namespace Annex.Data.Shared
{
    public class OffsetVector : Vector
    {
        public readonly Vector Original;
        public readonly Vector Offset;

        public override float X {
            get => this.Offset.X + this.Original.X;
            set => this.Offset.X = value;
        }

        public override float Y {
            get {
                return this.Offset.Y + this.Original.Y;
            }
            set {
                this.Offset.Y = value;
            }
        }

        public OffsetVector(Vector original, Vector offset) {
            this.Original = original;
            this.Offset = offset;
        }

        public OffsetVector(Vector original, float offsetX, float offsetY) {
            this.Original = original;
            this.Offset = Create(offsetX, offsetY);
        }
    }
}
