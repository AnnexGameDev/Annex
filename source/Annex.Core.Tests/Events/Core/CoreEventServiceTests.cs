using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Events.Core;
using Moq;
using System.Threading.Tasks;
using UnitTests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Events.Core
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