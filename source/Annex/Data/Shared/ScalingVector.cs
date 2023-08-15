namespace Annex_Old.Data.Shared
{
    public class ScalingVector : Vector
    {
        public readonly Vector Base;
        public readonly Vector Scale;

        public override float X {
            get => this.Base.X * this.Scale.X;
            set => this.Scale.X = value;
        }

        public override float Y {
            get => this.Base.Y * this.Scale.Y;
            set => this.Scale.Y = value;
        }

        public ScalingVector(Vector baseVector, float scaleX, float scaleY) : this(baseVector, Create(scaleX, scaleY)) {

        }

        public ScalingVector(Vector baseVector, Vector scaleVector) {
            this.Base = baseVector;
            this.Scale = scaleVector;
        }
    }
}
