using Annex_Old.Events;
using Annex_Old.Graphics;
using Annex_Old.Graphics.Events;
using Annex_Old.Services;

namespace Annex_Old.Scenes.Components
{
    public class Scene : Container
    {
        public readonly EventQueue EventQueue;
        public UIElement? FocusObject { get; private set; }

        public Scene(int width, int height) {
            this.FocusObject = null;
            this.EventQueue = new EventQueue();
            this.Position.Set(0, 0);
            this.Size.Set(width, height);
        }

        public Scene() {
            this.FocusObject = null;
            this.EventQueue = new EventQueue();
            this.Position.Set(0, 0);
            this.Size.Set(ServiceProvider.Canvas.GetResolution());
        }

        public override void Draw(ICanvas canvas) {
            if (!this.Visible) {
                return;
            }
            base.Draw(canvas);
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e) {
            if (e.Handled) {
                if (this.FocusObject == this) {
                    base.HandleKeyboardKeyReleased(e);
                } else {
                    this.FocusObject?.HandleKeyboardKeyReleased(e);
                }
            }
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            if (!e.Handled) {
                if (this.FocusObject == this) {
                    base.HandleKeyboardKeyPressed(e);
                } else {
                    this.FocusObject?.HandleKeyboardKeyPressed(e);
                }
            }
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            if (!e.Handled) {
                if (this.FocusObject == this) {
                    base.HandleMouseButtonPressed(e);
                } else {
                    this.FocusObject?.HandleMouseButtonPressed(e);
                }
            }
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e) {
            if (!e.Handled) {
                if (this.FocusObject == this) {
                    base.HandleMouseButtonReleased(e);
                } else {
                    this.FocusObject?.HandleMouseButtonReleased(e);
                }
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
