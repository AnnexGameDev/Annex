using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Platform
{
    public class GraphicsEngine
    {
        public static IGraphicsEngine? _graphicsEngine = null;

        public GraphicsEngine(IGraphicsEngine graphicsEngine) {
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
