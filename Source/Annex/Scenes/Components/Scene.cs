#nullable enable
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Events;

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

        public override void Draw(ICanvas canvas) {
            if (!this.Visible) {
                return;
            }
            base.Draw(canvas);
        }

        internal override bool HandleSceneFocusMouseDown(int x, int y) {
            if (this.FocusObject != null) {
                this.FocusObject.IsFocus = false;
            }
            this.FocusObject = null;
            return base.HandleSceneFocusMouseDown(x, y);
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            if (!e.Handled) {
                this.FocusObject?.HandleKeyboardKeyPressed(e);
            }
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            if (!e.Handled) {
                this.FocusObject?.HandleMouseButtonPressed(e);
            }
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e) {
            if (!e.Handled) {
                this.FocusObject?.HandleMouseButtonReleased(e);
            }
        }
    }
}
