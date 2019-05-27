namespace Annex.Data.ReferenceTypes
{
    public class PString
    {
        public string? Value { get; private set; }

        public PString() {
            this.Value = null;
        }

        public PString(PString copy) {
            this.Value = copy.Value;
        }

        public PString(string value) {
            this.Value = value;
        }

        public static implicit operator string?(PString pstr) {
            return pstr?.Value;
        }

        public void Set(string value) {
            this.Value = value;
        }
    }
}
