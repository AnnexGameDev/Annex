#nullable enable

namespace Annex.Data.Shared
{
    public class String
    {
        public string? Value { get; private set; }

        public String() {

        }

        public String(String copy) {
            this.Value = copy.Value;
        }

        public String(string? value) {
            this.Value = value;
        }

        public static implicit operator string?(String pstr) {
            return pstr?.Value;
        }

        public static implicit operator String(string? value) {
            if (value == null) {
                return new String();
            }
            return new String(value);
        }

        public void Set(string value) {
            this.Value = value;
        }
    }
}
