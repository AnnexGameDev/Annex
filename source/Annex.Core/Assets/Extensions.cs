namespace Annex.Core.Assets
{
    internal static class Extensions
    {
        public static string ToSafeAssetIdString(this string id) {
            return id.Replace('\\', '/');
        }
    }
}
