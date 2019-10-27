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

        public static Vector Create() {
            return new Vector_Value_Value(0, 0);
        }

        public static Vector Create(float x, float y) {
            return new Vector_Value_Value(x, y);
        }

        public static Vector Create(Float x, float y) {
            return new Vector_Reference_Value(x, y);
        }

        public static Vector Create(float x, Float y) {
            return new Vector_Value_Reference(x, y);
        }

        public static Vector Create(Float x, Float y) {
            return new Vector_Reference_Reference(x, y);
        }

        private class Vector_Value_Value : Vector
        {
            public Vector_Value_Value(float x, float y) {
                this.X = x;
                this.Y = y;
            }
        }

        private class Vector_Value_Reference : Vector
        {
            private float _x;
            private readonly Float _y;

            public override float X { get => this._x; set => this._x = value; }
            public override float Y { get => this._y; set => this._y.Set(value); }

            public Vector_Value_Reference(float x, Float y) {
                this.X = x;
                this._y = y;
            }
        }

        private class Vector_Reference_Value : Vector
        {
            private readonly Float _x;
            private float _y;

            public override float X { get => this._x; set => this._x.Set(value); }
            public override float Y { get => this._y; set => this._y = value; }

            public Vector_Reference_Value(Float x, float y) {
                this._x = x;
                this.Y = y;
            }
        }

        private class Vector_Reference_Reference : Vector
        {
            private readonly Float _x;
            private readonly Float _y;

            public override float X { get => this._x; set => this._x.Set(value); }
            public override float Y { get => this._y; set => this._y.Set(value); }

            public Vector_Reference_Reference(Float x, Float y) {
                this._x = x;
                this._y = y;
            }
        }
    }
}
