using System.Collections.Generic;

namespace Annex.Scenes.Layouts.Html
{
    public class HtmlElementToAnnexTypeMap
    {
        private readonly Dictionary<string, string> _map;
        public string this[string key] => this._map[key.ToLower()];

        public HtmlElementToAnnexTypeMap(params string[] oneToOneNames) {
            this._map = new Dictionary<string, string>();
            foreach (var name in oneToOneNames) {
                this.AddRelation(name, name);
            }
        }

        public void AddRelation(string elementName, string typeName) {
            this._map.Add(elementName.ToLower(), typeName.ToCamelCaseWord());
        }

        public bool ContainsElement(string elementName) {
            return this._map.ContainsKey(elementName);
        }
    }
}
