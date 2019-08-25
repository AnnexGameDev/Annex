using Annex.Events;
using Annex.Graphics;
using Annex.Scenes;
using Annex.Scenes.Components;
using Annex.Scenes.Scenes;
using SampleProject.Data;
using SampleProject.Scenes.MainMenu.Buttons;
using SampleProject.Scenes.MainMenu.Textboxes;

namespace SampleProject.Scenes.MainMenu
{
    public class MainMenu : Scene
    {
        private readonly DataManager _data;

        public MainMenu() {
            this._data = DataManager.Singleton;

            this.AddChild(new SampleButton());
            this.AddChild(new SampleTextbox());
            this.Events.AddEvent(PriorityType.INPUT, this.HandlePlayerMovement, 1);
        }

        public override void DrawGameContent(IDrawableContext surfaceContext) {
            this._data.Map.Draw(surfaceContext);
            this._data.Player.Draw(surfaceContext);
        }

        public override void HandleCloseButtonPressed() {
            SceneManager.Singleton.LoadScene<GameClosing>();
        }

        private ControlEvent HandlePlayerMovement() {
            float speed = 3;
            var player = this._data.Player;
            var window = GameWindow.Singleton;
            var context = window.Context;

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
                window.Context.GetCamera().ZoomIn(0.01f);
            }
            if (context.IsKeyDown(KeyboardKey.E)) {
                window.Context.GetCamera().ZoomOut(0.01f);
            }

            if (context.IsKeyDown(KeyboardKey.W)) {
                player.EntitySize.X += 1;
                player.EntitySize.Y += 1;
            }

            if (context.IsKeyDown(KeyboardKey.S)) {
                player.EntitySize.X += -1;
                player.EntitySize.Y += -1;
            }
            return ControlEvent.NONE;
        }
    }
}
