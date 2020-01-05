using Annex;
using SampleProject.Scenes.Level1;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args) {
            AnnexGame.Initialize();
            AnnexGame.Start<Level1>();
        }
    }
}