using Annex.Events;
using Annex.Services;
using System;
using static Annex.Graphics.EventIDs;

namespace Annex.Graphics.Sfml.Events
{
    public class GraphicsEvent : GameEvent {
        private readonly ICanvas _canvas;
        private readonly Action _predraw;
        private readonly Action _postdraw;

        public GraphicsEvent(ICanvas canvas, Action preDraw, Action postDraw) : base(DrawGameEventID, 0, 0) {
            this._canvas = canvas;
            this._predraw = preDraw;
            this._postdraw = postDraw;
        }

        protected override void Run(Annex.Events.EventArgs gameEventArgs) {
            this._predraw.Invoke();
            ServiceProvider.SceneService.CurrentScene.Draw(this._canvas);
            this._postdraw.Invoke();
        }
    }
}
