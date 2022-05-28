using Annex_Old.Core.Graphics;
using Annex_Old.Core.Helpers;
using Annex_Old.Core.Scenes.Components;
using FluentAssertions;
using Moq;
using Scaffold.Platform;
using Scaffold.Tests.Core.Fixture;
using System;
using Xunit;

namespace Annex_Old.Core.Tests.Scenes.Components
{
    public class TextboxTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IClipboardService> _clipboardServiceMock = new();
        private readonly Mock<IGraphicsEngine> _graphicsEngineMock = new();

        public TextboxTests() {
            this._fixture.Register(_fixture.Create<TextBox>);

            new ClipboardHelper(_clipboardServiceMock.Object);
            new GraphicsEngineHelper(_graphicsEngineMock.Object);
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
