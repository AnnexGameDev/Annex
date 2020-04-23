#nullable enable

namespace Annex.Data.Shared
{
    public class String : Shared<string?>
    {
        public String() {

        }

        public String(string? value) {
            this.Value = value;
        }

        public static implicit operator String(string? value) {
            if (value == null) {
                return new String();
            }
            return new String(value);
        }

        public static implicit operator string?(String instance) {
            return instance.Value;
        }
    }
}
