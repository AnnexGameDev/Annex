﻿using System.IO;
using System.Xml.Serialization;

namespace Annex.Data.Serialization
{
    public static class XML
    {
        public static bool XmlDeserialize<T>(string path, out T result) {
            var xml = new XmlSerializer(typeof(T));
            
            if (!File.Exists(path)) {
                result = default;
                return false;
            }

            using var fs = new FileStream(path, FileMode.OpenOrCreate);
            result = (T)xml.Deserialize(fs);
            return true;
        }

        public static void XmlSerialize<T>(T instance, string path) {
            var xml = new XmlSerializer(typeof(T));
            using var fs = new FileStream(path, FileMode.Create);
            xml.Serialize(fs, instance);
        }
    }
}
