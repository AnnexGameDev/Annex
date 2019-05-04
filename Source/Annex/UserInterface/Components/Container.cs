using Annex.Graphics;
using System.Collections.Generic;

namespace Annex.UserInterface.Components
{
    public class Container : UIElement
    {
        public readonly List<UIElement> Children;

        public Container() {
            this.Children = new List<UIElement>();
        }

        public override void Draw(IDrawableContext surfaceContext) {
            foreach (var child in this.Children) {
                child.Draw(surfaceContext);
            }
        }
    }
}
