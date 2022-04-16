using Annex.Core.Data;
using Annex.Core.Graphics;
using Scaffold.Collections.Generic;

namespace Annex.Core.Scenes.Components
{
    public class Container : Image, IParentElement
    {
        private ConcurrentList<IUIElement> _children = new();

        public Container(string elementId, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
        }

        public IEnumerable<IUIElement> Children => _children;

        public void AddChild(IUIElement child) {
            this._children.Add(child);
        }

        protected override void DrawInternal(ICanvas canvas) {
            base.DrawInternal(canvas);
            foreach (var child in this._children) {
                child.Draw(canvas);
            }
        }
    }
}
