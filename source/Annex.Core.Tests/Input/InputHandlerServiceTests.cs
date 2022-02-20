using Annex.Core.Graphics.Windows;
using Annex.Core.Input;
using Annex.Core.Input.InputEvents;
using Annex.Core.Scenes;
using Moq;
using UnitTests.Core.Attributes;
using UnitTests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Input
{
    public class InputHandlerServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<ISceneService> _theSceneServiceMock;
        private readonly Mock<IScene> _theCurrentSceneMock;

        public InputHandlerServiceTests() {
            this._theSceneServiceMock = this._fixture.Freeze<Mock<ISceneService>>();
            this._theCurrentSceneMock = this._fixture.Create<Mock<IScene>>();
            this._theSceneServiceMock.Setup(theSceneService => theSceneService.CurrentScene).Returns(this._theCurrentSceneMock.Object);

            this._fixture.Register<IInputHandlerService>(this._fixture.Create<InputHandlerService>);
        }

        [Theory, AutoMoqData]
        public void GivenAKeyPressed_WhenHandlingTheKeyboardKeyPressed_ThenTheCurrentSceneHandlesTheEvent(IWindow aGivenWindow, KeyboardKey aGivenPressedKey) {
            // Arrange
            var theInputHandlerService = this._fixture.Create<IInputHandlerService>();

            // Act
            theInputHandlerService.HandleKeyboardKeyPressed(aGivenWindow, aGivenPressedKey);

            // Assert
            this._theCurrentSceneMock.Verify(theCurrentScene => theCurrentScene.OnKeyboardKeyPressed(aGivenWindow, It.Is<KeyboardKeyPressedEvent>(e => e.Key == aGivenPressedKey)), Times.Once);
        }

        [Theory, AutoMoqData]
        public void GivenAKeyPressed_WhenHandlingTheKeyboardKeyReleased_ThenTheCurrentSceneHandlesTheEvent(IWindow aGivenWindow, KeyboardKey aGivenReleasedKey) {
            // Arrange
            var theInputHandlerService = this._fixture.Create<IInputHandlerService>();

            // Act
            theInputHandlerService.HandleKeyboardKeyReleased(aGivenWindow, aGivenReleasedKey);

            // Assert
            this._theCurrentSceneMock.Verify(theCurrentScene => theCurrentScene.OnKeyboardKeyReleased(aGivenWindow, It.Is<KeyboardKeyReleasedEvent>(e => e.Key == aGivenReleasedKey)), Times.Once);
        }

        [Theory, AutoMoqData]
        public void GivenAWindow_WhenHandlingTheWindowClosing_ThenTheCurrentSceneHandlesTheEvent(IWindow aGivenWindow) {
            // Arrange
            var theInputHandlerService = this._fixture.Create<IInputHandlerService>();

            // Act
            theInputHandlerService.HandleWindowClosed(aGivenWindow);

            // Assert
            this._theCurrentSceneMock.Verify(theCurrentScene => theCurrentScene.OnWindowClosed(aGivenWindow), Times.Once);
        }

        [Theory, AutoMoqData]
        public void GivenTheMousePressesAGivenButtonAtAGivenWindowLocation_WhenHandlingTheMouseButtonPressed_ThenTheCurrentSceneHandlesTheEvent(IWindow aGivenWindow, MouseButton aGivenMouseButton, int aGivenWindowX, int aGivenWindowY) {
            // Arrange
            var theInputHandlerService = this._fixture.Create<IInputHandlerService>();

            // Act
            theInputHandlerService.HandleMouseButtonPressed(aGivenWindow, aGivenMouseButton, aGivenWindowX, aGivenWindowY);

            // Assert
            this._theCurrentSceneMock.Verify(theCurrentScene => theCurrentScene.OnMouseButtonPressed(aGivenWindow, It.Is<MouseButtonPressedEvent>(e => e.Button == aGivenMouseButton && e.WindowX == aGivenWindowX && e.WindowY == aGivenWindowY)), Times.Once);
        }

        [Theory, AutoMoqData]
        public void GivenTheMouseReleasesAGivenButtonAtAGivenWindowLocation_WhenHandlingTheMouseButtonReleased_ThenTheCurrentSceneHandlesTheEvent(IWindow aGivenWindow, MouseButton aGivenMouseButton, int aGivenWindowX, int aGivenWindowY) {
            // Arrange
            var theInputHandlerService = this._fixture.Create<IInputHandlerService>();

            // Act
            theInputHandlerService.HandleMouseButtonReleased(aGivenWindow, aGivenMouseButton, aGivenWindowX, aGivenWindowY);

            // Assert
            this._theCurrentSceneMock.Verify(theCurrentScene => theCurrentScene.OnMouseButtonReleased(aGivenWindow, It.Is<MouseButtonReleasedEvent>(e => e.Button == aGivenMouseButton && e.WindowX == aGivenWindowX && e.WindowY == aGivenWindowY)), Times.Once);
        }

        [Theory, AutoMoqData]
        public void GivenTheMouseMovesAtAGivenWindowLocation_WhenHandlingTheMouseMoved_ThenTheCurrentSceneHandlesTheEvent(IWindow aGivenWindow, int aGivenWindowX, int aGivenWindowY) {
            // Arrange
            var theInputHandlerService = this._fixture.Create<IInputHandlerService>();

            // Act
            theInputHandlerService.HandleMouseMoved(aGivenWindow, aGivenWindowX, aGivenWindowY);

            // Assert
            this._theCurrentSceneMock.Verify(theCurrentScene => theCurrentScene.OnMouseMoved(aGivenWindow, It.Is<MouseMovedEvent>(e => e.WindowX == aGivenWindowX && e.WindowY == aGivenWindowY)), Times.Once);
        }

        [Theory, AutoMoqData]
        public void GivenTheMouseScrollWheelMovedAGivenDelta_WhenHandlingTheMouseScrollWheelMoved_ThenTheCurrentSceneHandlesTheEvent(IWindow aGivenWindow, double aGivenDelta) {
            // Arrange
            var theInputHandlerService = this._fixture.Create<IInputHandlerService>();

            // Act
            theInputHandlerService.HandleMouseScrollWheelMoved(aGivenWindow, aGivenDelta);

            // Assert
            this._theCurrentSceneMock.Verify(theCurrentScene => theCurrentScene.OnMouseScrollWheelMoved(aGivenWindow, It.Is<MouseScrollWheelMovedEvent>(e => e.Delta == aGivenDelta)), Times.Once);
        }
    }
}