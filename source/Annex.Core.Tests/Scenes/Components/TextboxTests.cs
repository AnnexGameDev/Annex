using Annex.Core.Graphics;
using Annex.Core.Platform;
using Annex.Core.Scenes.Components;
using FluentAssertions;
using Moq;
using Scaffold.Platform;
using Scaffold.Tests.Core.Fixture;
using System;
using Xunit;

namespace Annex.Core.Tests.Scenes.Components
{
    public class TextboxTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IClipboardService> _clipboardServiceMock = new();
        private readonly Mock<IGraphicsEngine> _graphicsEngineMock = new();

        public TextboxTests() {
            this._fixture.Register(_fixture.Create<TextBox>);

            new Clipboard(_clipboardServiceMock.Object);
            new GraphicsEngine(_graphicsEngineMock.Object);
        }

        [Fact]
        public void GivenATextbox_WhenAddingAChild_ThenThrowsNotSupportedException() {
            // Arrange
            var theTextbox = _fixture.Create<TextBox>();

            // Act
            Action theAddChildAction = () => theTextbox.AddChild(It.IsAny<IUIElement>());

            // Assert
            theAddChildAction.Should().Throw<NotSupportedException>();
        }
    }
}
