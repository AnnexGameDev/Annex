namespace Annex.Events.Execution
{
    public interface IExecutionEngine
    {
        void Execute(IEvent e);
    }
}
