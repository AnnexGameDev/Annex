namespace Annex.Data.Shared
{
    public class ScalingOffsetVector : OffsetVector
    {
        public readonly Vector Scale;

        public override float X {
            get => this.Original.X + (this.Offset.X * this.Scale.X);
            set => this.Scale.X = value;
        }
        public override float Y {
            get => this.Original.Y + (this.Offset.Y * this.Scale.Y);
            set => this.Scale.Y = value;
        }

        public ScalingOffsetVector(Vector original, Vector offset, float xScale, float yScale) : this(original, offset, Create(xScale, yScale)) {
        }

        public ScalingOffsetVector(Vector original, Vector offset, Vector scale) : base(original, offset) {
            this.Scale = scale;
        }
    }
}
