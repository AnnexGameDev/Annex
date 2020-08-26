namespace Annex.Data.Shared
{
    public class Shared<T>
    {
        public virtual T Value { get; set; }

        public Shared() {

        }

        public Shared(T value) {
            this.Value = value;
        }

        public Shared(Shared<T> copy) {
            this.Value = copy.Value;
        }

        public void Set(T value) {
            this.Value = value;
        }
    }

    public class Shared<X_Type, Y_Type>
    {
        public virtual X_Type X { get; set; }
        public virtual Y_Type Y { get; set; }

        public Shared() {

        }

        public Shared(X_Type x, Y_Type y) {
            this.X = x;
            this.Y = y;
        }

        public void Set(X_Type x, Y_Type y) {
            this.X = x;
            this.Y = y;
        }

        public void Set(Shared<X_Type, Y_Type> v) {
            this.X = v.X;
            this.Y = v.Y;
        }
    }
}
