using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Helpers
{
    public class GraphicsEngineHelper
    {
        public static IGraphicsEngine? _graphicsEngine = null;

        public GraphicsEngineHelper(IGraphicsEngine graphicsEngine) {
            _graphicsEngine = graphicsEngine;
        }


        public static FloatRect GetTextBounds(TextContext textContext, bool forceContextUpdate = true) {
            return _graphicsEngine!.GetTextBounds(textContext, forceContextUpdate);
        }

        public static float GetCharacterX(TextContext textContext, int index, bool forceContextUpdate = true) {
            return _graphicsEngine!.GetCharacterX(textContext, index, forceContextUpdate);
        }
    }
}
