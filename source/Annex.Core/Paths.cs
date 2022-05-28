using Scaffold;

namespace Annex_Old.Core
{
    public static class Paths
    {
        public static string GetParentFolderWithFile(string fileName) {
            var di = new DirectoryInfo(ScaffoldApp.ApplicationPath);
            while (di != null) {
                if (Directory.GetFiles(di.FullName).Any(filePath => filePath.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase))) {
                    return di.FullName;
                }
                di = di.Parent;
            }
            throw new FileNotFoundException($"Unable to find directory with the file {fileName}");
        }
    }
}