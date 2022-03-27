namespace Annex.Core.Events.Core
{
    public enum CoreEventPriority : long
    {
        Networking = 0,
        UserInput = 1000,
        GameLogic = 2000,
        Graphics = 3000,
        Audio = 4000,
    }
}