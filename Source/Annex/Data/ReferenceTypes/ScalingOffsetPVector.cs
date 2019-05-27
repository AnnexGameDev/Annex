namespace Annex.Data.ReferenceTypes
{
    public class ScalingOffsetPVector : OffsetPVector
    {
        public readonly PVector Scale;

        public override float X {
            get => this.Original.X + (this.Offset.X * this.Scale.X);
            set => this.Scale.X = value;
        }
        public override float Y {
            get => this.Original.Y + (this.Offset.Y * this.Scale.Y);
            set => this.Scale.Y = value;
        }

        public ScalingOffsetPVector(PVector original, PVector offset, float xScale, float yScale) : this(original, offset, new PVector(xScale, yScale)) {
        }

        public ScalingOffsetPVector(PVector original, PVector offset, PVector scale) : base(original, offset) {
            this.Scale = scale;
        }
    }
}
