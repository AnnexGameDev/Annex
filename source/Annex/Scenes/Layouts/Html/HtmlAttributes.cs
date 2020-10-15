using Annex.Data.Shared;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Annex.Scenes.Layouts.Html
{
    public class HtmlAttributes
    {
        private Dictionary<string, string> _attributes;

        public string? ID => this.TryGetValue("id", out string id) ? id : null;
        public string NodeName { get; private set; }

        public HtmlAttributes(XElement element) {
            this._attributes = new Dictionary<string, string>();

            this.NodeName = element.Name.LocalName;

            foreach (var entry in element.Attributes()) {
                this._attributes[entry.Name.LocalName] = entry.Value;
            }
        }

        public void RemoveAttribute(string key) {
            if (this._attributes.ContainsKey(key)) {
                this._attributes.Remove(key);
            }
        }

        public bool TryGetValue(string key, out string value) {
            if (this._attributes.TryGetValue(key, out var val)) {
                value = val;
                return true;
            }
            value = string.Empty;
            return false;
        }

        public string this[string key] => this._attributes[key];

        public bool SetXYComponents(string key, Vector target, Vector multiplier, Vector offset) {
            if (!this.TryGetValue(key, out string value)) {
                return false;
            }

            var data = value.Split(',');
            string x_str = data[0].Trim();
            string y_str = data[1].Trim();

            float x = this.ToFloat(x_str, multiplier.X, offset.X);
            float y = this.ToFloat(y_str, multiplier.Y, offset.Y);
            target.Set(x, y);
            return true;
        }

        private float ToFloat(string data, float percentageMultiplier, float offset) {
            float baseFloat = 0;

            if (data.EndsWith("%")) {
                baseFloat = float.Parse(data[..^1]) * percentageMultiplier / 100.0f;
            } else if (data.Length != 0) {
                baseFloat = float.Parse(data);
            }

            return baseFloat + offset;
        }
    }
}
