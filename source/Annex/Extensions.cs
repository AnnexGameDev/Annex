namespace Annex
{
    public static class Extensions
    {
        public static string Format(this string str, params object[] args) {
            return string.Format(str, args);
        }

        public static string ToCamelCaseWord(this string str) {
            return $"{char.ToUpper(str[0])}{str[1..].ToLower()}";
        }
    }
}
