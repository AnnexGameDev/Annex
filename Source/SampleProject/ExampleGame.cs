using Annex;
using Annex.UserInterface;
using SampleProject.Data;
using SampleProject.Scenes.MainMenu;

namespace SampleProject
{
    public class Game
    {
        private static void Main() {
            var game = new AnnexGame();

            Singleton.Create<DataManager>();
            Singleton.Get<UI>().LoadScene<MainMenu>();

            game.Start();
        }
    }
}
