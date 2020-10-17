using System.Collections.Generic;

namespace Annex.Data.Serialization
{
    public class JsonArray : IEnumerable<JsonElement>
    {
        private readonly List<JsonElement> _elements;

        public JsonArray() {
            this._elements = new List<JsonElement>();
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
    }
}
