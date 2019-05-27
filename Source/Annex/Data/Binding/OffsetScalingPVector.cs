namespace Annex.Data.Binding
{
    public class OffsetScalingPVector : ScalingPVector
    {
        public readonly PVector Offset;

        public override float X {
            get => base.X + this.Offset.X;
            set => this.Offset.X = value;
        }
        public override float Y {
            get => base.Y + this.Offset.Y;
            set => this.Offset.Y = value;
        }

        public OffsetScalingPVector(PVector original, PVector offset, PVector scale) : base(original, scale) {
            this.Offset = offset;
        }

        public OffsetScalingPVector(PVector original, PVector scale, float offsetX, float offsetY) : this(original, new PVector(offsetX, offsetY), scale) {

        }
    }
}
