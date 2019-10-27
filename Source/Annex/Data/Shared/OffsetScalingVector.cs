namespace Annex.Data.Shared
{
    public class OffsetScalingVector : ScalingVector
    {
        public readonly Vector Offset;

        public override float X {
            get => base.X + this.Offset.X;
            set => this.Offset.X = value;
        }
        public override float Y {
            get => base.Y + this.Offset.Y;
            set => this.Offset.Y = value;
        }

        public OffsetScalingVector(Vector original, Vector offset, Vector scale) : base(original, scale) {
            this.Offset = offset;
        }

        public OffsetScalingVector(Vector original, Vector scale, float offsetX, float offsetY) : this(original, Create(offsetX, offsetY), scale) {

        }
    }
}
