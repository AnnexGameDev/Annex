using Annex.Events;
using Annex.Events.Execution;
using Annex.Events.Scheduling;
using NUnit.Framework;
using System.Threading;

namespace IntegrationTests
{
    public class EventServiceTests
    {
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
        public void EventService_TerminatorIsNeverTriggered_DoesntTerminate() {
            var terminator = new BooleanTerminator();

            var worker = new Thread(() => {
                this.EventService.Run(terminator);
                Assert.Fail();
            });
            worker.Start();

            Thread.Sleep(1000);
            worker.Interrupt();
            Assert.Pass();
        }

        [Test]
        public void EventService_TerminatorIsTriggered_Terminates() {
            var terminator = new BooleanTerminator();

            var worker = new Thread(() => {
                Thread.Sleep(1000);
                terminator.Toggle();
            });
            worker.Start();
            this.EventService.Run(terminator);
            Assert.Pass();
        }

        private class BooleanTerminator : ITerminatable
        {
            private bool _value;

            public void Toggle() {
                this._value = !this._value;
            }

            public BooleanTerminator(bool value = false) {
                this._value = value;
            }

            public bool ShouldTerminate() {
                return this._value;
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
