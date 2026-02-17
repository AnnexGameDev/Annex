using Annex.Core.Time;

namespace Annex.Core.Events;

public class GameEvent : IGameEvent
{
    private long _lastProbe;
    private readonly Action _action;
    private readonly int _interval;
    private readonly ITimeService _timeService;

    public GameEvent(ITimeService timeService, Action action, int interval)
    {
        _action = action;
        _interval = interval;
        _timeService = timeService;
    }

    public void Probe()
    {
        if (_timeService.ElapsedTimeSince(_lastProbe) >= _interval)
        {
            _action();
            _lastProbe = _timeService.Now;
        }
    }
}