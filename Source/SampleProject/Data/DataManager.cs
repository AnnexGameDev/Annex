using Annex;
using SampleProject.Data.Map;

namespace SampleProject.Data
{
    public class DataManager : Singleton
    {
        public Player Player = new Player();
        public BasicMap Map = new BasicMap();

        static DataManager() {
            Create<DataManager>();
        }
        public static DataManager Singleton => Get<DataManager>();
    }
}
