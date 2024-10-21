
namespace Annex.Core.Graphics.Windows;

public class DrawWindowEvent : Events.Event
{
    private readonly IWindow _window;
    private readonly Action<ICanvas> _drawAction;

    public DrawWindowEvent(IWindow window, Action<ICanvas> drawAction, int interval) : base(interval, 0)
    {
        _window = window;
        _drawAction = drawAction;
    }

    protected override Task RunAsync()
    {
        var canvas = _window.GetCanvas();
        canvas.PreDraw();
        _drawAction(canvas);
        canvas.PostDraw();
        return Task.CompletedTask;
    }
}
