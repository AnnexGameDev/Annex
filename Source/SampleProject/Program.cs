using Annex;
using SampleProject.Scenes.Level1;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args) {
            AnnexGame.Initialize();
            Debug.PackageResourcesToBinary();
            AnnexGame.Start<Level1>();
        }
    }
}