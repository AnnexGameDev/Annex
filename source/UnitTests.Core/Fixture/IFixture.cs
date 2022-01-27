namespace UnitTests.Core.Fixture
{
    public interface IFixture
    {
        T Freeze<T>();
        T Create<T>();
    }
}
