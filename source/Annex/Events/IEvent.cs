namespace Annex.Events
{
    public interface IEvent
    {
        EventArgs Probe(long timeDelta);
    }
}
