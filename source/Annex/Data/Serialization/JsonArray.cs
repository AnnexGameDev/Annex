using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Annex_Old.Data.Serialization
{
    public class JsonArray : IEnumerable<JsonElement>
    {
        private readonly List<JsonElement> _elements;

        public JsonArray() {
            this._elements = new List<JsonElement>();
        }

        public JsonArray(JArray jarray) : this() {
            foreach (var element in jarray) {
                this._elements.Add(new JsonElement(element.ToString()));
            }
        }

        public void Add(JsonElement element) {
            this._elements.Add(element);
        }

        IEnumerator<JsonElement> IEnumerable<JsonElement>.GetEnumerator() {
            return this._elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this._elements.GetEnumerator();
        }

        public int Count() {
            return this._elements.Count;
        }
    }
}
