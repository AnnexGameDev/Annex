using Annex.Core.Time;

namespace Annex.Core.Events;

public class GameEvent : IGameEvent
{
    private long _lastProbe;
    private readonly Func<Task> _task;
    private readonly int _interval;
    private readonly ITimeService _timeService;

    public GameEvent(ITimeService timeService, Func<Task> task, int interval)
    {
        _task = task;
        _interval = interval;
        _timeService = timeService;
    }

    public async Task ProbeAsync()
    {
        if (_timeService.ElapsedTimeSince(_lastProbe) >= _interval)
        {
            await _task();
            _lastProbe = _timeService.Now;
        }
    }
}