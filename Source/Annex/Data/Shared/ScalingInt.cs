namespace Annex.Data.Shared
{
    public class ScalingInt : Int
    {
        public readonly Int Original;
        public readonly Int Scale;

        public override int Value {
            get => this.Original.Value * this.Scale.Value;
            set => this.Scale.Value = value;
        }

        public ScalingInt(Int original, Int scale) {
            this.Original = original;
            this.Scale = scale;
        }
    }
}
