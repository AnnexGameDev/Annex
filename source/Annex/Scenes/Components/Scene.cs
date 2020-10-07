using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Services;

namespace Annex.Scenes.Components
{
    public class Scene : Container
    {
        public readonly EventQueue Events;
        public UIElement? FocusObject { get; private set; }

        public Scene(int width, int height) {
            this.FocusObject = null;
            this.Events = new EventQueue();
            this.Position.Set(0, 0);
            this.Size.Set(width, height);
        }

        public Scene() {
            this.FocusObject = null;
            this.Events = new EventQueue();
            this.Position.Set(0, 0);
            this.Size.Set(ServiceProvider.Canvas.GetResolution());
        }

        public override void Draw(ICanvas canvas) {
            if (!this.Visible) {
                return;
            }
            base.Draw(canvas);
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

        public void ChangeFocusObject(UIElement? uielement) {
            this.FocusObject?.LostFocus();
            this.FocusObject = uielement;
            this.FocusObject?.GainedFocus();
        }

        public virtual void OnEnter(OnSceneEnterEvent e) {
        }

        public virtual void OnLeave(OnSceneLeaveEvent e) {
        }
    }
}
