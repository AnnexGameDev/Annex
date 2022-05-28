namespace Annex_Old.Core.Data
{
    public class OffsetVector2f : IVector2<float>
    {
        public IVector2<float> BaseVector { get; }
        public IVector2<float> OffsetVector { get; private set; }

        public float X => this.BaseVector.X + this.OffsetVector.X;
        public float Y => this.BaseVector.Y + this.OffsetVector.Y;

        public OffsetVector2f(IVector2<float> baseVector, float xOffset, float yOffset) : this(baseVector, new Vector2f(xOffset, yOffset)) {

        }

        public OffsetVector2f(IVector2<float> baseVector, IVector2<float> offsetVector) {
            this.BaseVector = baseVector;
            this.OffsetVector = offsetVector;
        }

        public void Set(IVector2<float> vector) {
            this.OffsetVector = vector;
        }

        public void Set(float x, float y) {
            throw new NotImplementedException($"{nameof(OffsetVector2f)} doesn't support {nameof(Set)}");
        }
    }
}
