using Newtonsoft.Json.Linq;

namespace Annex_Old.Data.Serialization
{
    public static class Json
    {
        public static string Serialize(IJsonSerializable serializable) {
            return serializable.GetJson().ToString();
        }

        public static T Deserialize<T>(T instance, string json) where T : IJsonSerializable {
            var jobject = new JsonElement(json);
            instance.SetFromJson(jobject);
            return instance;
        }
    }
}
