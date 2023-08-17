using Annex.Core.Graphics;
using Annex.Core.Scenes.Elements;
using Moq;
using Scaffold.Platform;
using Scaffold.Tests.Core.Fixture;

namespace Annex.Core.Tests.Scenes.Components
{
    public class TextboxTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IClipboardService> _clipboardServiceMock = new();
        private readonly Mock<IGraphicsEngine> _graphicsEngineMock = new();

        public TextboxTests() {
            this._fixture.Register(_fixture.Create<Textbox>);
        }
    }
}
