using Annex;
using Annex.Graphics;
using Annex.UserInterface;
using SampleProject.Data;
using SampleProject.Scenes;

namespace SampleProject
{
    public class Game : AnnexGame
    {
        private DataManager Data;

        private static void Main(string[] args) {
            new Game().Start();
        }

        public Game() {
            Data = Singleton.Create<DataManager>();
            Singleton.Get<UI>().LoadScene<MainMenu>();
        }

        public override void DrawGame(IDrawableContext context) {
            Data.Player.Draw(context);
        }
    }
}
