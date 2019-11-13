using Annex;
using SampleProject.Scenes.Level1;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args) {
            var game = new AnnexGame();

            game.Start<Level1>();
        }
    }
}