using Annex.Events;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Annex.Graphics
{
    public abstract class InputHandler
    {
        private Scene currentScene => ServiceProvider.SceneService.CurrentScene;
        private bool _preventEvents => !ServiceProvider.Canvas.IsActive;

        private float _lastMouseClickX;
        private float _lastMouseClickY;
        private long _lastMouseClick;

        public void JoystickMoved(JoystickMovedEvent e) {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleJoystickMoved(e);
        }

        public void JoystickDisconnected(JoystickDisconnectedEvent e) {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleJoystickDisconnected(e);
        }

        public void JoystickConnected(JoystickConnectedEvent e) {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleJoystickConnected(e);
        }

        public void JoystickButtonReleased(JoystickButtonReleasedEvent e) {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleJoystickButtonReleased(e);
        }

        public void JoystickButtonPressed(JoystickButtonPressedEvent e) {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleJoystickButtonPressed(e);
        }

        public void MouseButtonReleased(MouseButtonReleasedEvent e) {
            if (this._preventEvents) {
                return;
            }
            e.TimeSinceClick = EventService.CurrentTime - this._lastMouseClick;
            this.currentScene.HandleMouseButtonReleased(e);
        }

        public void MouseButtonPressed(MouseButtonPressedEvent e) {
            if (this._preventEvents) {
                return;
            }

            bool doubleClick = false;
            float dx = e.MouseX - this._lastMouseClickX;
            float dy = e.MouseY - this._lastMouseClickY;
            long dt = EventService.CurrentTime - this._lastMouseClick;
            int distanceThreshold = 10;
            int timeThreshold = 250;

            if (Math.Sqrt(dx * dx + dy * dy) < distanceThreshold && dt < timeThreshold) {
                doubleClick = true;
            }

            this._lastMouseClickX = e.MouseX;
            this._lastMouseClickY = e.MouseY;
            this._lastMouseClick = EventService.CurrentTime;
            e.DoubleClick = doubleClick;

            this.currentScene.HandleSceneFocusMouseDown(e.MouseX, e.MouseY);
            this.currentScene.HandleMouseButtonPressed(e);
        }

        public void KeyReleased(KeyboardKeyReleasedEvent e) {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleKeyboardKeyReleased(e);
        }

        public void KeyPressed(KeyboardKeyPressedEvent e) {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleKeyboardKeyPressed(e);
        }

        public void OnClosed() {
            if (this._preventEvents) {
                return;
            }
            this.currentScene.HandleCloseButtonPressed();
        }
    }
}
