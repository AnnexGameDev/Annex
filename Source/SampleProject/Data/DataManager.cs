using Annex;
using SampleProject.Data.Map;

namespace SampleProject.Data
{
    public class DataManager : Singleton
    {
        public Player Player = new Player();
        public BasicMap Map = new BasicMap();
    }
}
