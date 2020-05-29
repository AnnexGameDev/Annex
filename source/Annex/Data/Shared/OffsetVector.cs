namespace Annex.Data.Shared
{
    public class OffsetVector : Vector
    {
        public readonly Vector Base;
        public readonly Vector Offset;

        public override float X {
            get => this.Offset.X + this.Base.X;
            set => this.Offset.X = value;
        }

        public override float Y {
            get => this.Offset.Y + this.Base.Y;
            set => this.Offset.Y = value;
        }

        public OffsetVector(Vector baseVector, Vector offsetVector) {
            this.Base = baseVector;
            this.Offset = offsetVector;
        }

        public OffsetVector(Vector baseVector, float offsetX, float offsetY) {
            this.Base = baseVector;
            this.Offset = Create(offsetX, offsetY);
        }
    }
}
