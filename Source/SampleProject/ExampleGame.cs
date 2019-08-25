using Annex;
using Annex.Graphics;
using Annex.Scenes;
using SampleProject.Data;
using SampleProject.Scenes.MainMenu;

namespace SampleProject
{
    public class Game
    {
        private static void Main() {
            var game = new AnnexGame();

            SceneManager.Singleton.LoadScene<MainMenu>();
            GameWindow.Singleton.Context.GetCamera().Follow(DataManager.Singleton.Player);

            game.Start();
        }
    }
}
