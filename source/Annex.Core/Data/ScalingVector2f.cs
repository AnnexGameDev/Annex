namespace Annex.Core.Data
{
    public class ScalingVector2f : IVector2<float>
    {
        public IVector2<float> BaseVector { get; }
        public IVector2<float> ScaleVector { get; }

        public float X => this.BaseVector.X * this.ScaleVector.X;
        public float Y => this.BaseVector.Y * this.ScaleVector.Y;

        public ScalingVector2f(IVector2<float> baseVector, float xScale, float yScale) : this(baseVector, new Vector2f(xScale, yScale)) {

        }

        public ScalingVector2f(IVector2<float> baseVector, IVector2<float> scaleVector) {
            this.BaseVector = baseVector;
            this.ScaleVector = scaleVector;
        }
    }
}
