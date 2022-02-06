using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
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
            this._graphicsEngineMock.Verify(graphicsEngine => graphicsEngine.CreateWindow(WindowStyle.Default), Times.Once);
            theCreatedWindow.Should().NotBeNull();
        }

        [Fact]
        public void GivenASize_WhenCreatingAWindowWithTheGivenSize_ThenAWindowIsCreatedWithTheGivenSize() {
            // Arrange
            var aWindowMock = this._fixture.Freeze<Mock<IWindow>>();
            this._graphicsEngineMock.Setup(graphicsEngine => graphicsEngine.CreateWindow(It.IsAny<WindowStyle>())).Returns(aWindowMock.Object);

            string aGivenId = this._fixture.Create<string>();
            var aGivenSize = this._fixture.Create<Vector2ui>();

            // Act
            var theCreatedWindow = this._graphicsService.CreateWindow(aGivenId, size: aGivenSize);

            // Assert
            theCreatedWindow.Should().Be(aWindowMock.Object);
            aWindowMock.VerifySet(window => window.Size = aGivenSize);
        }

        [Fact]
        public void GivenAPosition_WhenCreatingAWindowWithTheGivenPosition_ThenAWindowIsCreatedWithTheGivenPosition() {
            // Arrange
            var aWindowMock = this._fixture.Freeze<Mock<IWindow>>();
            this._graphicsEngineMock.Setup(graphicsEngine => graphicsEngine.CreateWindow(It.IsAny<WindowStyle>())).Returns(aWindowMock.Object);

            string aGivenId = this._fixture.Create<string>();
            var aGivenPosition = this._fixture.Create<Vector2i>();

            // Act
            var theCreatedWindow = this._graphicsService.CreateWindow(aGivenId, position: aGivenPosition);

            // Assert
            theCreatedWindow.Should().Be(aWindowMock.Object);
            aWindowMock.VerifySet(window => window.Position = aGivenPosition);
        }

        [Fact]
        public void GivenATitle_WhenCreatingAWindowWithTheGivenTitle_ThenAWindowIsCreatedWithTheGivenTitle() {
            // Arrange
            string aGivenId = this._fixture.Create<string>();
            string aGivenTitle = this._fixture.Create<string>();

            var aWindowMock = this._fixture.Freeze<Mock<IWindow>>();
            this._graphicsEngineMock.Setup(graphicsEngine => graphicsEngine.CreateWindow(It.IsAny<WindowStyle>())).Returns(aWindowMock.Object);

            // Act
            var theCreatedWindow = this._graphicsService.CreateWindow(aGivenId, title: aGivenTitle);

            // Assert
            theCreatedWindow.Should().Be(aWindowMock.Object);
            aWindowMock.VerifySet(window => window.Title = aGivenTitle);
        }

        [Fact]
        public void GivenAStyle_WhenCreatingAWindowWithTheGivenStyle_ThenAWindowIsCreatedWithTheGivenStyle() {
            // Arrange
            string aGivenId = this._fixture.Create<string>();
            var aGivenStyle = this._fixture.Create<WindowStyle>();

            var aWindowMock = this._fixture.Freeze<Mock<IWindow>>();
            this._graphicsEngineMock.Setup(graphicsEngine => graphicsEngine.CreateWindow(It.IsAny<WindowStyle>())).Returns(aWindowMock.Object);

            // Act
            var theCreatedWindow = this._graphicsService.CreateWindow(aGivenId, style: aGivenStyle);

            // Assert
            theCreatedWindow.Should().NotBeNull();
            this._graphicsEngineMock.Verify(graphicsEngine => graphicsEngine.CreateWindow(aGivenStyle), Times.Once);
        }
    }
}