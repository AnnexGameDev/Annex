using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Annex.Scenes.Components
{
    internal class DebugOverlay : UIElement
    {
        private SolidRectangleContext _background;
        internal static List<Func<string>> _informationRetrievers;
        private TextContext _information;

        static DebugOverlay() {
            _informationRetrievers = new List<Func<string>>();
        }

        public DebugOverlay(string id) : base(id) {
            this._background = new SolidRectangleContext(new RGBA(0, 0, 0, 150)) {
                RenderSize = Vector.Create(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT),
                UseUIView = true
            };
            this._information = new TextContext("", "OpenSans-Regular.ttf") {
                FontSize = 16,
                BorderColor = RGBA.Black,
                BorderThickness = 2,
                FontColor = RGBA.White,
                UseUIView = true
            };
        }

        public override void Draw(ICanvas canvas) {

            var sb = new StringBuilder();
            foreach (var ir in _informationRetrievers) {
                sb.AppendLine(ir.Invoke());
            }
            _information.RenderText.Set(sb.ToString());

            canvas.Draw(this._background);
            canvas.Draw(this._information);
        }
    }
}
