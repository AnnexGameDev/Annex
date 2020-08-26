using System;
using System.IO;
using System.Linq;

namespace Annex
{
    public static class Paths
    {
        public static string ApplicationFolder => AppContext.BaseDirectory;
        public static string SolutionFolder => GetSolutionFolder();

        private static string GetSolutionFolder() {
            var di = new DirectoryInfo(ApplicationFolder);
            while (di.Parent != null) {
                if (Directory.GetFiles(di.FullName, "*.sln").Any()) {
                    break;
                }
                di = di.Parent;
            }
            return di.FullName;
        }
    }
}
