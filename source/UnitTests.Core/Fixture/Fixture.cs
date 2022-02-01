using AutoFixture;
using AutoFixture.AutoMoq;

namespace UnitTests.Core.Fixture
{
    public class Fixture : IFixture
    {
        private readonly global::AutoFixture.Fixture _fixture;

        public Fixture() {
            this._fixture = new global::AutoFixture.Fixture();
            this._fixture.Customize(new AutoMoqCustomization());
        }

        public T Create<T>() {
            return this._fixture.Create<T>();
        }

        public IEnumerable<T> CreateMany<T>() {
            return this._fixture.CreateMany<T>();
        }

        public T Freeze<T>() {
            return this._fixture.Freeze<T>();
        }

        public void Register<T>(Func<T> creator) {
            this._fixture.Register<T>(creator);
        }
    }
}