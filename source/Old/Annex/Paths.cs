using System;
using System.IO;
using System.Linq;

namespace Annex
{
    public static class Paths
    {
        public static string ApplicationPath => AppContext.BaseDirectory;
        public static string SourceFolder => Path.Combine(GetSolutionFolder(), "source");
        public static string SolutionFolder => GetSolutionFolder();

        private static string GetSolutionFolder() {
            var di = new DirectoryInfo(ApplicationPath);
            while (di.Parent != null) {
                if (Directory.GetFiles(di.FullName, "*.sln").Select(filePath => new FileInfo(filePath)).Any(fi => fi.Name == "Annex.sln")) {
                    break;
                }
                di = di.Parent;
            }
            return di.FullName;
        }
    }
}
