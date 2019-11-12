#nullable enable
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;

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
            if (!this.Visible) {
                return;
            }
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

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            this.FocusObject?.HandleKeyboardKeyPressed(e);
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            this.FocusObject?.HandleMouseButtonPressed(e);
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e) {
            this.FocusObject?.HandleMouseButtonReleased(e);
        }
    }
}
