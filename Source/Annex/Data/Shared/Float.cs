namespace Annex.Data.Shared
{
    public class Float
    {
        public virtual float Value { get; set; }

        public Float() {

        }

        public Float(float value) {
            this.Value = value;
        }

        public Float(Float copy) {
            this.Value = copy.Value;
        }

        public void Set(float val) {
            this.Value = val;
        }

        public static implicit operator float(Float pfloat) {
            return pfloat.Value;
        }

        public static explicit operator Float(float val) {
            return new Float(val);
        }
    }
}
