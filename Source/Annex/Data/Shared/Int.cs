namespace Annex.Data.Shared
{
    public class Int
    {
        public virtual int Value { get; set; }

        public Int() {

        }

        public Int(int value) {
            this.Value = value;
        }

        public Int(Int copy) {
            this.Value = copy.Value;
        }

        public void Set(int val) {
            this.Value = val;
        }

        public static implicit operator int(Int pint) {
            return pint.Value;
        }

        public static implicit operator Int(int val) {
            return new Int(val);
        }

        public static explicit operator uint(Int val) {
            return (uint)val.Value;
        }
    }
}
