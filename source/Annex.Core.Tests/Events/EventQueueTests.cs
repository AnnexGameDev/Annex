﻿using Annex.Core.Events;
using Annex.Core.Time;
using Moq;
using System.Linq;
using UnitTests.Core;
using UnitTests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Events
{
    public class EventQueueTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<ITimeService> _timeServiceMock;

        public EventQueueTests() {
            this._timeServiceMock = this._fixture.Freeze<Mock<ITimeService>>();
            this._fixture.Register<IEventQueue>(this._fixture.Create<EventQueue>);
        }

        [Fact]
        public void GivenQueueItems_WhenRunningTheQueue_ThenEachQueueItemIsRun() {
            // Arrange
            var eventMocks = this._fixture.CreateMany<Mock<IEvent>>();
            var eventScheduler = this._fixture.Create<IEventQueue>();
            eventScheduler.Add(eventMocks.Objects().ToArray());

            this._timeServiceMock
                .Setup(timeService => timeService.ElapsedTimeSince(It.IsAny<long>()))
                .Callback(() => eventScheduler.Stop());

            // Act
            eventScheduler.Run();

            // Assert
            eventMocks.VerifyMany(@event => @event.TimeElapsed(It.IsAny<long>()), Times.Once());
        }

        [Fact]
        public void GivenQueueItemsAreRemoved_WhenRunningTheQueue_ThenNoQueueItemIsRun() {
            // Arrange
            var eventMock = this._fixture.Create<Mock<IEvent>>();

            var eventScheduler = this._fixture.Create<IEventQueue>();
            eventScheduler.Add(eventMock.Object);

            eventScheduler.Remove(eventMock.Object.Id);

            this._timeServiceMock
                .Setup(timeService => timeService.ElapsedTimeSince(It.IsAny<long>()))
                .Callback(() => eventScheduler.Stop());

            // Act
            eventScheduler.Run();

            // Assert
            eventMock.Verify(@event => @event.TimeElapsed(It.IsAny<long>()), Times.Never);
        }
    }
}