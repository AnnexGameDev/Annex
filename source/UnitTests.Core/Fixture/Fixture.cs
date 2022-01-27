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

        public T Freeze<T>() {
            return this._fixture.Freeze<T>();
        }
    }
}