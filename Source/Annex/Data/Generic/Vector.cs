namespace Annex.Data.Shared
{
    public abstract class Vector
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }

        public void Set(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public void Set(Vector v) {
            this.X = v.X;
            this.Y = v.Y;
        }

        public void Add(float x, float y) {
            this.X += x;
            this.Y += y;
        }

        public void Add(Vector v) {
            this.X += v.X;
            this.Y += v.Y;
        }

        public static implicit operator SFML.System.Vector2f(Vector source) {
            return new SFML.System.Vector2f(source.X, source.Y);
        }

        private protected Vector() {

        }

        public static Vector Create(float x, float y) {
            return new VectorVV(x, y);
        }

        public static Vector Create(Float x, float y) {
            return new VectorPV(x, y);
        }

        public static Vector Create(float x, Float y) {
            return new VectorVP(x, y);
        }

        public static Vector Create(Float x, Float y) {
            return new VectorPP(x, y);
        }
    }

    public class VectorVV : Vector
    {
        private float _x;
        private float _y;

        public override float X { get => this._x; set => this._x = value; }
        public override float Y { get => this._y; set => this._y = value; }

        public VectorVV(float x, float y) {
            this.X = x;
            this.Y = y;
        }
    }

    public class VectorVP : Vector
    {
        private float _x;
        private readonly Float _y;

        public override float X { get => this._x; set => this._x = value; }
        public override float Y { get => this._y; set => this._y.Set(value); }

        public VectorVP(float x, Float y) {
            this.X = x;
            this.Y = y;
        }
    }

    public class VectorPV : Vector
    {
        private readonly Float _x;
        private float _y;

        public override float X { get => this._x; set => this._x.Set(value); }
        public override float Y { get => this._y; set => this._y = value; }

        public VectorPV(Float x, float y) {
            this.X = x;
            this.Y = y;
        }
    }

    public class VectorPP : Vector
    {
        private readonly Float _x;
        private readonly Float _y;

        public override float X { get => this._x; set => this._x.Set(value); }
        public override float Y { get => this._y; set => this._y.Set(value); }

        public VectorPP(Float x, Float y) {
            this.X = x;
            this.Y = y;
        }
    }
}
