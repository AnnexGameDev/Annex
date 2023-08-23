namespace Annex.Core.Events;

public interface IThreadService
{
    Task<T> RunOnBackgroundThreadAsync<T>(Func<Task<T>> work);
    Task RunOnBackgroundThreadAsync(Func<Task> work);
    void StartOnBackgroundThread(Action work);
}

internal class ThreadService : IThreadService
{
    public void StartOnBackgroundThread(Action work) {
        var thread = new Thread(() => work());
        thread.Start();
    }

    public Task<T> RunOnBackgroundThreadAsync<T>(Func<Task<T>> work) {
        return Task.Run(work);
    }

    public Task RunOnBackgroundThreadAsync(Func<Task> work) {
        return Task.Run(work);
    }
}
