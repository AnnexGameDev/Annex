using Annex.Core.Events;
using SampleProject.Models;
using System.Threading.Tasks;

namespace SampleProject.Scenes.Level1.Events
{
    public class PlayerAnimationEvent : Event
    {
        private readonly Player _player;

        public PlayerAnimationEvent(Player player, int interval_ms) : base(interval_ms, 0) {
            this._player = player;
        }

        protected override Task RunAsync() {
            this._player.Animate();
            return Task.CompletedTask;
        }
    }
}
