using Annex.Core.Events;
using Moq;
using Scaffold.Tests.Core;
using Scaffold.Tests.Core.Fixture;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Annex.Core.Tests.Events
{
    public class EventQueueTests
    {
        private readonly IFixture _fixture = new Fixture();

        public EventQueueTests() {
            this._fixture.Register<IEventQueue>(this._fixture.Create<EventQueue>);
        }

        [Fact]
        public async Task GivenQueueItems_WhenRunningTheQueue_ThenEachQueueItemIsRun() {
            // Arrange
            var eventMocks = this._fixture.CreateMany<Mock<IEvent>>();
            var eventScheduler = this._fixture.Create<IEventQueue>();
            eventScheduler.Add(eventMocks.Objects().ToArray());

            // Act
            await eventScheduler.StepAsync();

            // Assert
            eventMocks.VerifyAll(@event => @event.TimeElapsedAsync(It.IsAny<long>()), Times.Once());
        }

        [Fact]
        public async Task GivenQueueItemsAreRemoved_WhenRunningTheQueue_ThenNoQueueItemIsRun() {
            // Arrange
            var eventMock = this._fixture.Create<Mock<IEvent>>();

            var eventScheduler = this._fixture.Create<IEventQueue>();
            eventScheduler.Add(eventMock.Object);
            eventScheduler.Remove(eventMock.Object.Id);

            // Act
            await eventScheduler.StepAsync();

            // Assert
            eventMock.Verify(@event => @event.TimeElapsedAsync(It.IsAny<long>()), Times.Never);
        }
    }
}