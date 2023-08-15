namespace Annex.Core.Data
{
    public class Shared<T>
    {
        public T Value { get; set; }

        public Shared(T value) {
            Value = value;
        }

        public void Set(T value) {
            this.Value = value;
        }

        public static implicit operator Shared<T>(T value) {
            return new Shared<T>(value);
        }

        public static explicit operator T(Shared<T> shared) {
            return shared.Value;
        }
    }
}
