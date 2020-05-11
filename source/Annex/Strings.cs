using System;
using System.IO;
using System.Linq;

namespace Annex
{
    public static class Strings
    {
        internal static class Errors
        {
            internal static class Audio
            {
                internal const string RESOURCE_LOAD_FAILED = "Failed to load audio resource {0}";

                internal static class Sfml
                {
                    internal const string BUFFERMODE_NOT_SUPPORTED = "BufferMode {0} is unsupported";
                    internal const string PERFORMED_OPERATION_WHILE_DISPOSED = "Tried to perform operation {0} in a disposed state";
                    internal const string INVALID_RESOURCE = "Resource must be a sound or music and not {0}";
                    internal const string INVALID_VOLUME_VALUE = "Invalid value for volume {0}. Must be in the range [0,100]";
                }
            }
        }

        public static class Paths
        {
            public static string ApplicationPath { get; private set; }
            public static string SolutionFolder { get; private set; }

            static Paths() {
                ApplicationPath = AppContext.BaseDirectory;
                SolutionFolder = GetSolutionFolder();
            }

            private static string GetSolutionFolder() {
                var di = new DirectoryInfo(ApplicationPath);
                while (di.Parent != null) {
                    if (Directory.GetFiles(di.FullName, "*.sln").Select(filePath => new FileInfo(filePath)).Any(fi => fi.Name == "Annex.sln")) {
                        di = new DirectoryInfo(Path.Combine(di.FullName, "source"));
                        break;
                    }
                    di = di.Parent;
                }
                return di.FullName;
            }
        }
    }

    internal static class Extensions
    {
        public static string Format(this string str, params object[] args) {
            return string.Format(str, args);
        }
    }
}
