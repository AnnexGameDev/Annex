using SFML.System;

namespace Annex.Data.Shared
{
    public abstract class Vector : Shared<float, float>
    {
        public void Add(float x, float y) {
            this.X += x;
            this.Y += y;
        }

        public void Add(Vector instance) {
            this.X += instance.X;
            this.Y += instance.Y;
        }

        public static implicit operator Vector2f(Vector instance) {
            return new Vector2f(instance.X, instance.Y);
        }

        public static Vector Create() {
            return new Vector_Val_Val();
        }

        public static Vector Create(float x, float y) {
            return new Vector_Val_Val(x, y);
        }

        public static Vector Create(Float x, float y) {
            return new Vector_Ref_Val(x, y);
        }

        public static Vector Create(float x, Float y) {
            return new Vector_Val_Ref(x, y);
        }

        public static Vector Create(Float x, Float y) {
            return new Vector_Ref_Ref(x, y);
        }

        private class Vector_Val_Val : Vector
        {
            public Vector_Val_Val() {

            }

            public Vector_Val_Val(float x, float y) {
                this.X = x;
                this.Y = y;
            }
        }

        private class Vector_Ref_Val : Vector
        {
            private readonly Float _x;
            public override float X { get => this._x.Value; set => this._x.Value = value; }

            public Vector_Ref_Val() {
                this._x = new Float();
            }

            public Vector_Ref_Val(Float x, float y) {
                this._x = x;
                this.Y = y;
            }
        }

        private class Vector_Val_Ref : Vector
        {
            private readonly Float _y;
            public override float Y { get => this._y.Value; set => this._y.Value = value; }

            public Vector_Val_Ref() {
                this._y = new Float();
            }

            public Vector_Val_Ref(float x, Float y) {
                this.X = x;
                this._y = y;
            }
        }

        private class Vector_Ref_Ref : Vector
        {
            private readonly Float _x;
            public override float X { get => this._x.Value; set => this._x.Value = value; }

            private readonly Float _y;
            public override float Y { get => this._y.Value; set => this._y.Value = value; }

            public Vector_Ref_Ref() {
                this._x = new Float();
                this._y = new Float();
            }

            public Vector_Ref_Ref(Float x, Float y) {
                this._x = x;
                this._y = y;
            }
        }
    }
}
