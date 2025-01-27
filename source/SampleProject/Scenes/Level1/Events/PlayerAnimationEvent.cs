using SampleProject.Models;
using System.Threading.Tasks;

namespace SampleProject.Scenes.Level1.Events;

public class PlayerAnimationEvent
{
    private readonly Player _player;

    public PlayerAnimationEvent(Player player)
    {
        this._player = player;
    }

    public Task RunAsync()
    {
        this._player.Animate();
        return Task.CompletedTask;
    }
}
