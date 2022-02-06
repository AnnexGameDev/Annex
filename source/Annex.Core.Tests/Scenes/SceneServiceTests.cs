using Annex.Core.Scenes;
using Annex.Core.Scenes.Components;
using Annex.Core.Services;
using FluentAssertions;
using Moq;
using System;
using UnitTests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Scenes
{
    public class SceneServiceTests
    {
        private readonly Mock<IContainer> _containerMock;
        private readonly ISceneService _sceneService;

        public SceneServiceTests() {
            var fixture = new Fixture();

            this._containerMock = fixture.Freeze<Mock<IContainer>>();
            this._sceneService = fixture.Create<SceneService>();
        }

        private Mock<T> RegisterScene<T>() where T : class, IScene {
            var sceneMock = new Mock<T>();
            this._containerMock.Setup(container => container.Resolve<T>()).Returns(sceneMock.Object);
            return sceneMock;
        }

        [Fact]
        public void GivenNoSceneLoaded_WhenGettingCurrentScene_ThenThrowsNullReferenceException() {
            // Arrange
            // Act
            // Assert
            Assert.Throws<NullReferenceException>(() => {
                var _ = this._sceneService.CurrentScene;
            });
        }

        [Fact]
        public void GivenSceneIsLoaded_WhenGettingCurrentScene_ThenIsTheExpectedScene() {
            // Arrange
            this.RegisterScene<AGivenSceneType>();

            // Act
            this._sceneService.LoadScene<AGivenSceneType>();

            // Assert
            this._sceneService.CurrentScene.Should().BeAssignableTo<AGivenSceneType>();
            this._sceneService.IsCurrentScene<AGivenSceneType>().Should().BeTrue();
        }

        [Fact]
        public void GivenASceneIsLoaded_WhenLoadingANewScene_ThenOnLeaveIsCalledForTheOldSceneAndOnEnterIsCalledForTheNewScene() {
            // Arrange
            var anOldSceneMock = this.RegisterScene<IOldScene>();
            var aNewSceneMock = this.RegisterScene<INewScene>();

            this._sceneService.LoadScene<IOldScene>();

            // Act
            this._sceneService.LoadScene<INewScene>();

            // Assert
            anOldSceneMock.Verify(anOldScene => anOldScene.OnLeave(It.Is<OnSceneLeaveEventArgs>(args => args.NextScene == aNewSceneMock.Object)), Times.Once());
            aNewSceneMock.Verify(aNewScene => aNewScene.OnEnter(It.Is<OnSceneEnterEventArgs>(args => args.PreviousScene == anOldSceneMock.Object)), Times.Once());
        }

        public interface AGivenSceneType : IScene { }
        public interface IOldScene : IScene { }
        public interface INewScene : IScene { }
    }
}