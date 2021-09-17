﻿using Annex.Events.Trackers;

namespace Annex.Events
{
    public interface IEvent
    {
        string EventID { get; }
        EventArgs Probe(long timeDifference_ms);
        void MarkForRemoval();
        void AttachTracker(IEventTracker tracker);
    }
}
