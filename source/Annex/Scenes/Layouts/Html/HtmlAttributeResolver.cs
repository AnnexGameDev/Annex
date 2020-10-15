using Annex.Data.Shared;

namespace Annex.Scenes.Layouts.Html
{
    public class HtmlAttributeResolver
    {
        private readonly HtmlAttributes elementAttributes;
        private readonly HtmlAttributes? styleAttributes;

        public string NodeName => this.elementAttributes.NodeName;
        public string? ID => this.elementAttributes.ID;

        public HtmlAttributeResolver(HtmlAttributes elementAttributes, HtmlAttributes? styleAttributes) {
            this.elementAttributes = elementAttributes;
            this.styleAttributes = styleAttributes;
        }

        public bool TryGetValue(string key, out string value) {
            if (this.elementAttributes.TryGetValue(key, out value)) {
                return true;
            }
            
            if (this.styleAttributes != null) {
                return this.styleAttributes.TryGetValue(key, out value);
            }
            value = string.Empty;
            return false;
        }

        public bool SetXYComponents(string key, Vector target, Vector multiplier, Vector offset) {
            if (this.elementAttributes.SetXYComponents(key, target, multiplier, offset)) {
                return true;
            }

            if (this.styleAttributes != null) {
                return this.styleAttributes.SetXYComponents(key, target, multiplier, offset);
            }
            return false;
        }
    }
}
