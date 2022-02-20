using AutoFixture.AutoMoq;

namespace UnitTests.Core.Attributes
{
    public class AutoMoqDataAttribute : AutoFixture.Xunit2.AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(new AutoFixture.Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true })) {

        }
    }
}