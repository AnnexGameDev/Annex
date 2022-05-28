using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Graphics.Contexts;

namespace Annex_Old.Core.Helpers
{
    public class GraphicsEngineHelper
    {
        public static IGraphicsEngine? _graphicsEngine = null;

        public GraphicsEngineHelper(IGraphicsEngine graphicsEngine) {
            if (_graphicsEngine != null) {
                throw new InvalidOperationException("Static helper resource is already instanciated");
            }
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
