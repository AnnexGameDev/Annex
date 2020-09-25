using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Services;
using SFML.Graphics;
using SFML.Window;
using System;

namespace Annex.Graphics.Sfml
{
    public class SfmlInputHandler : InputHandler
    {
        public SfmlInputHandler(RenderWindow renderWindow) {
            renderWindow.Closed += this.RenderWindow_Closed;
            renderWindow.KeyPressed += this.RenderWindow_KeyPressed;
            renderWindow.KeyReleased += this.RenderWindow_KeyReleased;
            renderWindow.MouseButtonPressed += this.RenderWindow_MouseButtonPressed;
            renderWindow.MouseButtonReleased += this.RenderWindow_MouseButtonReleased;
            renderWindow.JoystickButtonPressed += this.RenderWindow_JoystickButtonPressed;
            renderWindow.JoystickButtonReleased += this.RenderWindow_JoystickButtonReleased;
            renderWindow.JoystickConnected += this.RenderWindow_JoystickConnected;
            renderWindow.JoystickDisconnected += this.RenderWindow_JoystickDisconnected;
            renderWindow.JoystickMoved += this.RenderWindow_JoystickMoved;
        }

        private void RenderWindow_JoystickMoved(object? sender, JoystickMoveEventArgs e) {
            this.JoystickMoved(new JoystickMovedEvent() {
                JoystickID = e.JoystickId,
                Axis = e.Axis.ToNonSFML(),
                Delta = e.Position
            });
        }

        private void RenderWindow_JoystickDisconnected(object? sender, JoystickConnectEventArgs e) {
            this.JoystickDisconnected(new JoystickDisconnectedEvent() {
                JoystickID = e.JoystickId
            });
        }

        private void RenderWindow_JoystickConnected(object? sender, JoystickConnectEventArgs e) {
            this.JoystickConnected(new JoystickConnectedEvent() {
                JoystickID = e.JoystickId
            });
        }

        private void RenderWindow_JoystickButtonReleased(object? sender, JoystickButtonEventArgs e) {
            this.JoystickButtonReleased(new JoystickButtonReleasedEvent() {
                JoystickID = e.JoystickId,
                Button = (JoystickButton)e.Button
            });
        }

        private void RenderWindow_JoystickButtonPressed(object? sender, JoystickButtonEventArgs e) {
            this.JoystickButtonPressed(new JoystickButtonPressedEvent() {
                JoystickID = e.JoystickId,
                Button = (JoystickButton)e.Button
            });
        }

        private void RenderWindow_MouseButtonReleased(object? sender, MouseButtonEventArgs e) {
            var canvas = ServiceProvider.Canvas;
            var mousePos = canvas.GetRealMousePos();
            var gamePos = canvas.GetGameWorldMousePos();

            this.MouseButtonReleased(new MouseButtonReleasedEvent() {
                Button = e.Button.ToNonSFML(),
                MouseX = (int)mousePos.X,
                MouseY = (int)mousePos.Y,
                WorldX = gamePos.X,
                WorldY = gamePos.Y
            });
        }

        private void RenderWindow_MouseButtonPressed(object? sender, MouseButtonEventArgs e) {
            var canvas = ServiceProvider.Canvas;
            var mousePos = canvas.GetRealMousePos();
            var gamePos = canvas.GetGameWorldMousePos();

            this.MouseButtonPressed(new MouseButtonPressedEvent() {
                Button = e.Button.ToNonSFML(),
                MouseX = (int)mousePos.X,
                MouseY = (int)mousePos.Y,
                WorldX = gamePos.X,
                WorldY = gamePos.Y
            });
        }

        private void RenderWindow_KeyReleased(object? sender, KeyEventArgs e) {
            this.KeyReleased(new KeyboardKeyReleasedEvent() {
                Key = e.Code.ToNonSFML(),
                ShiftDown = e.Shift
            });
        }

        private void RenderWindow_KeyPressed(object? sender, KeyEventArgs e) {
            this.KeyPressed(new KeyboardKeyPressedEvent() {
                Key = e.Code.ToNonSFML(),
                ShiftDown = e.Shift
            });
        }

        private void RenderWindow_Closed(object? sender, EventArgs e) {
            this.OnClosed();
        }
    }
}
