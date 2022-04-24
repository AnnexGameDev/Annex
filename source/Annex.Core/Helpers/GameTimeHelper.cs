using Annex.Core.Time;

namespace Annex.Core.Helpers
{
    public class GameTimeHelper
    {
        private static ITimeService? _timeService = null;

        public GameTimeHelper(ITimeService timeService) {
            _timeService = timeService;
        }

        public static long Now() {
            return _timeService!.Now;
        }

        public static long ElapsedTimeSince(long time) {
            return _timeService!.ElapsedTimeSince(time);
        }
    }
}
