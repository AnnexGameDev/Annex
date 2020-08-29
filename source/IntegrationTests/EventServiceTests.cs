using Annex.Events;
using Annex.Events.Execution;
using Annex.Events.Scheduling;
using NUnit.Framework;

namespace IntegrationTests
{
    public class EventServiceTests {
        private EventService? _eventService;
        private EventService EventService => this._eventService!;

        [SetUp]
        public void Setup() {
            var executionEngine = new MockExecutionEngine();
            var schedulingEngine = new MockSchedulingEngine();
            this._eventService = new EventService(executionEngine, schedulingEngine);
        }

        [TearDown]
        public void Cleanup() {
            this.EventService.Dispose();
        }

        [Test]
        public void EventService_TerminatorIsTriggered_Terminates() {
            var terminator = new TrueTerminator();
            this.EventService.Run(terminator);
        }

        private class TrueTerminator : ITerminatable
        {
            public bool ShouldTerminate() {
                return true;
            }
        }

        private class MockExecutionEngine : IExecutionEngine
        {
            public void Execute(IEvent e) {
            }
        }

        private class MockSchedulingEngine : ISchedulingEngine
        {
            private class MockEventSchedule : IEventSchedule
            {
                public bool HasNext => false;
                public IEvent Next => new MockEvent();

                private class MockEvent : IEvent
                {
                    public EventArgs Probe(long timeDelta) {
                        return new EventArgs();
                    }
                }
            }

            public IEventSchedule GetEventSchedule() {
                return new MockEventSchedule();
            }

            public void Schedule(IEvent e) {
            }
        }
    }
}
