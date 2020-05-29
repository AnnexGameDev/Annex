namespace Annex.Data.Shared
{
    public class Float : Shared<float>
    {
        public Float() {

        }

        public Float(float value) : base(value) {

        }

        public static implicit operator float(Float instance) {
            return instance.Value;
        }
    }
}
