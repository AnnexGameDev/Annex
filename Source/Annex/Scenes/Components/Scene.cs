using Annex.Events;
using Annex.Graphics;

namespace Annex.Scenes.Components
{
    public class Scene : Container
    {
        public readonly EventQueue Events;
        public UIElement? FocusObject { get; internal set; }

        public Scene() {
            this.FocusObject = null;
            this.Events = new EventQueue();
            this.Position.Set(0, 0);
            this.Size.Set(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT);
        }

        public override void Draw(IDrawableContext context) {
            this.DrawScene(context);
            base.Draw(context);
        }

        public virtual void DrawScene(IDrawableContext context) {

        }

        internal override bool HandleSceneFocusMouseDown(int x, int y) {
            if (this.FocusObject != null) {
                this.FocusObject.IsFocus = false;
            }
            this.FocusObject = null;
            return base.HandleSceneFocusMouseDown(x, y);
        }

        public override void HandleKeyboardKeyPressed(KeyboardKey key) {
            this.FocusObject?.HandleKeyboardKeyPressed(key);
        }
    }
}
