using Annex.Events;
using NUnit.Framework;

namespace Tests.Events
{
    public class EventQueueTests
    {
        private class EmptyGameEvent : GameEvent
        {
            public EmptyGameEvent(int interval_ms, int delay_ms = 0) : base(interval_ms, delay_ms) {
            }

            protected override void Run(EventArgs gameEventArgs) {
            }
        }

        private EventQueue _eventQueue;

        [SetUp]
        public void SetUp() {
            this._eventQueue = new EventQueue();
        }

        [Test]
        public void GetEvent_WithEvent_ReturnsEvent() {
            foreach (var priority in Priorities.All) {
                var e = new EmptyGameEvent(0);
                this._eventQueue.AddEvent((PriorityType)priority, e);
                Assert.AreEqual(e, this._eventQueue.GetEvent(e.EventID));
            }
        }

        [Test]
        public void GetEvent_WithoutEvent_ReturnsNull() {
            Assert.IsNull(this._eventQueue.GetEvent("test"));
        }

        [Test]
        public void GetPriority_WithEvents_ReturnsPriorityLevel() {
            var rng = new System.Random();
            foreach (var priority in Priorities.All) {
                var n = rng.Next(0, 100);

                for (int i = 0; i < n; i++) {
                    var e = new EmptyGameEvent(0);
                    this._eventQueue.AddEvent((PriorityType)priority, e);
                }

                var events = this._eventQueue.GetPriority(priority);
                Assert.AreEqual(n, events.Count);
            }
        }

        [Test]
        public void GetPriority_WithoutEvents_ReturnsEmpty() {
            foreach (var priority in Priorities.All) {
                Assert.AreEqual(0, this._eventQueue.GetPriority(priority).Count);
            }
        }
    }
}
