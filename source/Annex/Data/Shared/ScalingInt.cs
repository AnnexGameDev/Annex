namespace Annex.Data.Shared
{
    public class ScalingInt : Int
    {
        public readonly Int Base;
        public readonly Int Scale;

        public override int Value {
            get => this.Base.Value * this.Scale.Value;
            set => this.Scale.Value = value;
        }

        public ScalingInt(Int baseInt, Int scale) {
            this.Base = baseInt;
            this.Scale = scale;
        }
    }
}
