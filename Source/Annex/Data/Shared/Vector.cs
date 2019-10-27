//namespace Annex.Data.Shared
//{
//    public unsafe class Vector
//    {
//        private readonly Float _x;
//        private readonly Float _y;

//        public virtual float X {
//            get {
//                return this._x.Value;
//            }
//            set {
//                this._x.Value = value;
//            }
//        }
//        public virtual float Y {
//            get {
//                return this._y.Value;
//            }
//            set {
//                this._y.Value = value;
//            }
//        }

//        public Vector(Float x, float y) {
//            this._x = x;
//            this._y = new Float(y);
//        }

//        public Vector(float x, Float y) {
//            this._x = new Float(x);
//            this._y = y;
//        }

//        public Vector(Float x, Float y) {
//            this._x = new Float(x);
//            this._y = new Float(y);
//        }

//        public Vector() {
//            this._x = new Float();
//            this._y = new Float();
//        }

//        public Vector(float x, float y) {
//            this._x = new Float(x);
//            this._y = new Float(y);
//        }

//        public static implicit operator SFML.System.Vector2f(Vector source) {
//            return new SFML.System.Vector2f(source.X, source.Y);
//        }

//        public void Set(float x, float y) {
//            this.X = x;
//            this.Y = y;
//        }

//        public void Add(Vector vector) {
//            this.X += vector.X;
//            this.Y += vector.Y;
//        }

//        public void Add(float x, float y) {
//            this.X += x;
//            this.Y += y;
//        }

//        public void Set(Vector position) {
//            this.X = position.X;
//            this.Y = position.Y;
//        }
//    }
//}
