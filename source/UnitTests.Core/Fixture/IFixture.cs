namespace UnitTests.Core.Fixture
{
    public interface IFixture
    {
        T Freeze<T>();
        T Create<T>();
        void Register<T>(Func<T> creator);
        IEnumerable<T> CreateMany<T>();
    }
}
