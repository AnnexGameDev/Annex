using Newtonsoft.Json.Linq;

namespace Annex.Data.Serialization
{
    public class JsonElement
    {
        private JObject _jsonObject;

        public JsonElement() {
            this._jsonObject = new JObject();
        }

        private JsonElement(JObject obj) {
            this._jsonObject = obj;
        }

        public JsonElement(string json) {
            this._jsonObject = JObject.Parse(json);
        }

        public void AddChild(string key, JsonElement node) {
            this._jsonObject[key] = node._jsonObject;
        }

        public void AddChild(string key, JsonArray array) {
            var jarray = new JArray();
            foreach (var item in array) {
                jarray.Add(item._jsonObject);
            }
            this._jsonObject[key] = jarray;
        }

        public void Set(string name, string value) {
            this._jsonObject[name] = value;
        }

        public override string ToString() {
            return this._jsonObject.ToString();
        }

        public string GetString(string key) {
            if (!this._jsonObject.ContainsKey(key)) {
                return string.Empty;
            }
            return (string)this._jsonObject[key]!;
        }

        public void Set(string key, float value) {
            this._jsonObject[key] = value;
        }

        public void Set(string key, int value) {
            this._jsonObject[key] = value;
        }

        public int GetInt(string key) {
            if (!this._jsonObject.ContainsKey(key)) {
                return 0;
            }
            return int.Parse((string)this._jsonObject[key]!);
        }

        public float GetFloat(string key) {
            if (!this._jsonObject.ContainsKey(key)) {
                return 0;
            }
            return float.Parse((string)this._jsonObject[key]!);
        }

        public JsonElement GetChild(string key) {
            if (!this._jsonObject.ContainsKey(key)) {
                return new JsonElement();
            }
            return new JsonElement((JObject)this._jsonObject[key]!);
        }
    }
}
