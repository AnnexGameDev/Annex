namespace Annex.Data.Shared
{
    public class ScalingVector : Vector
    {
        public readonly Vector Original;
        public readonly Vector Scale;
        public override float X {
            get => this.Original.X * this.Scale.X;
            set => this.Scale.X = value;
        }
        public override float Y {
            get => this.Original.Y * this.Scale.Y;
            set => this.Scale.Y = value;
        }

        public ScalingVector(Vector original, float scaleX, float scaleY) : this(original, Create(scaleX, scaleY)) {

        }

        public ScalingVector(Vector original, Vector scale) {
            this.Original = original;
            this.Scale = scale;
        }
    }
}
