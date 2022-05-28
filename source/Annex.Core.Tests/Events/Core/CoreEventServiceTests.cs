using Annex_Old.Core.Broadcasts;
using Annex_Old.Core.Broadcasts.Messages;
using Annex_Old.Core.Events.Core;
using Moq;
using Scaffold.Tests.Core.Fixture;
using System.Threading.Tasks;
using Xunit;

namespace Annex_Old.Core.Tests.Events.Core
{
    public class CoreEventServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IBroadcast<RequestStopAppMessage>> _stopBroadcastMessageMock;

        public CoreEventServiceTests() {
            this._fixture.Register<ICoreEventService>(this._fixture.Create<CoreEventService>);
            this._stopBroadcastMessageMock = this._fixture.Freeze<Mock<IBroadcast<RequestStopAppMessage>>>();
        }

        [Fact(Timeout = 100)]
        public async Task GivenARunningCoreEventService_WhenPublishingAStopBroadcastMessage_ThenTheRunningCoreEventServiceStops() {
            // Arrange
            var theCoreEventService = this._fixture.Create<ICoreEventService>();
            var theStopBroadcastMessage = this._fixture.Create<RequestStopAppMessage>();
            var theCoreEventServiceRunTask = Task.Run(() => theCoreEventService.Run());

            // Act
            this._stopBroadcastMessageMock.Raise(requestStopappMessageBroadcast => requestStopappMessageBroadcast.OnBroadcastPublished += null, this, theStopBroadcastMessage);

            // Assert
            await theCoreEventServiceRunTask;
        }
    }
}