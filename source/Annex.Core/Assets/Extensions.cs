namespace Annex.Core.Assets
{
    internal static class Extensions
    {
        public static string ToSafeAssetIdString(this string id) {
            return id.Replace('\\', '/');
        }

        public static IAssetGroup SceneData(this IAssetService assetService) {
            if (assetService.TryGetAssetGroup(KnownAssetGroups.SceneDataGroupId, out var group)) {
                return group;
            }
            throw new NullReferenceException($"{nameof(SceneData)} asset group does not exist");
        }

        public static IAssetGroup Textures(this IAssetService assetService) {
            if (assetService.TryGetAssetGroup(KnownAssetGroups.TextureGroupId, out var group)) {
                return group;
            }
            throw new NullReferenceException($"{nameof(Textures)} asset group does not exist");
        }

        public static IAssetGroup Fonts(this IAssetService assetService) {
            if (assetService.TryGetAssetGroup(KnownAssetGroups.FontGroupId, out var group)) {
                return group;
            }
            throw new NullReferenceException($"{nameof(Fonts)} asset group does not exist");
        }
    }
}
