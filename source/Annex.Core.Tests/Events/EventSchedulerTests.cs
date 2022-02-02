using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Events;
using Annex.Core.Times;
using FluentAssertions;
using Moq;
using System.Linq;
using UnitTests.Core;
using UnitTests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Events
{
    public class EventSchedulerTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IBroadcast<RequestStopAppMessage>> _requestStopAppMessageMock;
        private readonly Mock<ITimeService> _timeServiceMock;

        public EventSchedulerTests() {
            this._requestStopAppMessageMock = this._fixture.Freeze<Mock<IBroadcast<RequestStopAppMessage>>>();
            this._timeServiceMock = this._fixture.Freeze<Mock<ITimeService>>();
            this._fixture.Register<IEventScheduler>(this._fixture.Create<EventScheduler>);
        }

        [Fact]
        public void GivenEventGroups_WhenRunningTheQueue_ThenEachGroupIsRun() {
            // Arrange
            var groupMocks = this._fixture.CreateMany<Mock<IEventGroup>>();
            this._fixture.Register(() => groupMocks.Select(groupMock => groupMock.Object));

            var eventScheduler = this._fixture.Create<IEventScheduler>();

            this._timeServiceMock
                .Setup(timeService => timeService.ElapsedTimeSince(It.IsAny<long>()))
                .Callback(() => this._requestStopAppMessageMock.Raise(message => message.OnBroadcastPublished += null, null, new RequestStopAppMessage()));

            // Act
            eventScheduler.Run();

            // Assert
            groupMocks.VerifyMany(group => group.Run(It.IsAny<long>()), Moq.Times.Once());
        }

        [Fact]
        public void GivenEventGroups_WhenRunningTheQueue_ThenEachGroupIsRunInOrderOfPriority() {
            // Arrange
            var groupMocks = this._fixture.CreateMany<Mock<IEventGroup>>();
            this._fixture.Register(() => groupMocks.Select(groupMock => groupMock.Object));

            var eventScheduler = this._fixture.Create<IEventScheduler>();

            this._timeServiceMock
                .Setup(timeService => timeService.ElapsedTimeSince(It.IsAny<long>()))
                .Callback(() => this._requestStopAppMessageMock.Raise(message => message.OnBroadcastPublished += null, null, new RequestStopAppMessage()));

            int? lastEventExecutionPriority = null;
            bool eventsWereExecutedInOrderOfPriority = true;
            groupMocks.SetupMany(group => group.Run(It.IsAny<int>()))
                .CallbackMany(group => {
                    if (lastEventExecutionPriority > group.Priority) {
                        eventsWereExecutedInOrderOfPriority = false;
                    }
                    lastEventExecutionPriority = group.Priority;
                });

            // Act
            eventScheduler.Run();

            // Assert
            eventsWereExecutedInOrderOfPriority.Should().BeTrue();

        }
    }
}