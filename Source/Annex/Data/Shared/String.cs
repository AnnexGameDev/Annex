namespace Annex.Data.Shared
{
    public class String
    {
        public string? Value { get; private set; }

        public String() {
            this.Value = null;
        }

        public String(String copy) {
            this.Value = copy.Value;
        }

        public String(string value) {
            this.Value = value;
        }

        public static implicit operator string?(String pstr) {
            return pstr?.Value;
        }

        public void Set(string value) {
            this.Value = value;
        }
    }
}
