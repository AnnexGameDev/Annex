using System;

namespace Annex.Data.Shared
{
    [Serializable]
    public class Int : Shared<int>
    {
        public Int() {

        }

        public Int(int value) : base(value) {

        }

        public static implicit operator Int(int value) {
            return new Int(value);
        }

        public static implicit operator int(Int instance) {
            return instance.Value;
        }
    }
}
