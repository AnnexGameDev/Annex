using Annex.Core.Events;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Components
{
    public class Scene : IScene
    {
        private bool disposedValue;

        public IPriorityEventQueue Events { get; }

        public Scene() {
            this.Events = new PriorityEventQueue();
        }

        public virtual void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs) {
        }

        public virtual void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs) {
        }

        public virtual void OnKeyboardKeyPressed(IWindow window, KeyboardKeyPressedEvent keyboardKeyPressedEvent) {
        }

        public virtual void OnKeyboardKeyReleased(IWindow window, KeyboardKeyReleasedEvent keyboardKeyReleasedEvent) {
        }

        public virtual void OnWindowClosed(IWindow window) {
        }

        public virtual void OnMouseButtonPressed(IWindow window, MouseButtonPressedEvent mouseButtonPressedEvent) {
        }

        public virtual void OnMouseButtonReleased(IWindow window, MouseButtonReleasedEvent mouseButtonReleasedEvent) {
        }

        public virtual void OnMouseMoved(IWindow window, MouseMovedEvent mouseMovedEvent) {
        }

        public virtual void OnMouseScrollWheelMoved(IWindow window, MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent) {
        }

        public virtual void Draw(ICanvas canvas) {
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Scene()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}