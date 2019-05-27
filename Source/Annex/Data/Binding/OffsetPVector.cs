namespace Annex.Data.Binding
{
    public class OffsetPVector : PVector
    {
        public readonly PVector Original;
        public readonly PVector Offset;

        public override float X {
            get => this.Offset.X + this.Original.X;
            set => this.Offset.X = value;
        }

        public override float Y {
            get {
                return this.Offset.Y + this.Original.Y;
            }
            set {
                this.Offset.Y = value;
            }
        }

        public OffsetPVector(PVector original, PVector offset) {
            this.Original = original;
            this.Offset = offset;
        }
    }
}
