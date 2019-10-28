using System.IO;
using System.Xml.Serialization;

namespace Annex
{
    public static class XML
    {
        public static bool Deserialize<T>(string path, out T result) {
            var xml = new XmlSerializer(typeof(T));
            
            if (!File.Exists(path)) {
                result = default;
                return false;
            }

            using var fs = new FileStream(path, FileMode.OpenOrCreate);
            result = (T)xml.Deserialize(fs);
            return true;
        }

        public static void Serialize<T>(T instance, string path) {
            var xml = new XmlSerializer(typeof(T));
            using var fs = new FileStream(path, FileMode.Create);
            xml.Serialize(fs, instance);
        }
    }
}
