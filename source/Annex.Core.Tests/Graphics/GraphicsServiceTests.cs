using Annex.Core.Graphics;
using FluentAssertions;
using Moq;
using UnitTests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Graphics
{
    public class GraphicsServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IGraphicsEngine> _graphicsEngineMock;
        private readonly IGraphicsService _graphicsService;

        public GraphicsServiceTests() {
            this._graphicsEngineMock = this._fixture.Freeze<Mock<IGraphicsEngine>>();
            this._graphicsService = this._fixture.Create<GraphicsService>();
        }

        [Fact]
        public void GivenAGraphicsEngine_WhenCreatingAWindow_ThenADefaultWindowIsCreated() {
            // Arrange
            string aGivenId = this._fixture.Create<string>();

            // Act
            var theCreatedWindow = this._graphicsService.CreateWindow(aGivenId);

            // Assert
            this._graphicsEngineMock.Verify(graphicsEngine => graphicsEngine.CreateWindow(), Times.Once);
            theCreatedWindow.Should().NotBeNull();
        }
    }
}