using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.UserInterface;
using Annex.UserInterface.Components;
using Annex.UserInterface.Scenes;
using SampleProject.Data;
using SampleProject.Scenes.MainMenu.Buttons;

namespace SampleProject.Scenes.MainMenu
{
    public class MainMenu : Scene
    {
        private readonly DataManager _data;

        public MainMenu() {
            this._data = Singleton.Get<DataManager>();
            this.RenderBoxSurface.Set("gui/backgrounds/menubackground.png");

            this.AddChild(new SampleButton());

            var queue = Singleton.Get<EventQueue>();
            var ui = Singleton.Get<UI>();

            queue.AddEvent(PriorityType.LOGIC, () => {
                if (ui.IsCurrentScene<MainMenu>()) {
                    HandlePlayerMovement();
                }
                return ControlEvent.NONE;
            }, 1);
        }

        public override void Draw(IDrawableContext surfaceContext) {
            base.Draw(surfaceContext);
            this._data.Player.Draw(surfaceContext);
        }

        public override void HandleCloseButtonPressed() {
            Singleton.Get<UI>().LoadScene<GameClosing>();
        }

        private void HandlePlayerMovement() {
            float speed = 3;
            var player = this._data.Player;
            var context = Singleton.Get<GameWindow>().Context;

            if (context.IsKeyDown(KeyboardKey.LShift) || context.IsKeyDown(KeyboardKey.RShift)) {
                speed = 6;
            }

            if (context.IsKeyDown(KeyboardKey.Left)) {
                player.EntityPosition.X -= speed;
            }
            if (context.IsKeyDown(KeyboardKey.Right)) {
                player.EntityPosition.X += speed;
            }
            if (context.IsKeyDown(KeyboardKey.Up)) {
                player.EntityPosition.Y -= speed;
            }
            if (context.IsKeyDown(KeyboardKey.Down)) {
                player.EntityPosition.Y += speed;
            }

            if (context.IsKeyDown(KeyboardKey.Q)) {
                Singleton.Get<GameWindow>().Context.GetCamera().ZoomIn(0.01f);
            }
            if (context.IsKeyDown(KeyboardKey.E)) {
                Singleton.Get<GameWindow>().Context.GetCamera().ZoomOut(0.01f);
            }
        }
    }
}
