namespace Annex.Data.Binding
{
    public class ScalingPVector : PVector
    {
        public readonly PVector Original;
        public readonly PVector Scale;
        public override float X {
            get => this.Original.X * this.Scale.X;
            set => this.Scale.X = value;
        }
        public override float Y {
            get => this.Original.Y * this.Scale.Y;
            set => this.Scale.Y = value;
        }

        public ScalingPVector(PVector original, float scaleX, float scaleY) : this(original, new PVector(scaleX, scaleY)) {

        }

        public ScalingPVector(PVector original, PVector scale) {
            this.Original = original;
            this.Scale = scale;
        }
    }
}
