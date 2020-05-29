namespace Annex
{
    public static class Extensions
    {
        public static string Format(this string str, params object[] args) {
            return string.Format(str, args);
        }
    }
}