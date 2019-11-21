using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Annex.Scenes.Components
{
    internal class DebugOverlay : UIElement
    {
        public const string ID = "Debug-Overlay";
        private SolidRectangleContext _background;
        internal static List<Func<string>> _informationRetrievers;
        private TextContext _information;

        internal static Dictionary<string, Action<string[]>> _commands;
        private static List<string> _pastCommands;
        private static int _commandPtr = -1;

        private static string UserInput;

        static DebugOverlay() {
            _informationRetrievers = new List<Func<string>>();
            _commands = new Dictionary<string, Action<string[]>>();
            _pastCommands = new List<string>();

            _informationRetrievers.Add(() => "CMD: " + UserInput + "\r\n");
            _commands.Add("clear", (input) => _pastCommands.Clear());
        }

        public DebugOverlay() : base(ID) {
            this._background = new SolidRectangleContext(new RGBA(0, 0, 0, 150)) {
                RenderSize = Vector.Create(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT),
                UseUIView = true
            };
            this._information = new TextContext("", "default.ttf") {
                FontSize = 16,
                BorderColor = RGBA.Black,
                BorderThickness = 2,
                FontColor = RGBA.White,
                UseUIView = true
            };
        }

        internal static void AddCommand(string commandName, Action<string[]> worker) {
            _commands[commandName.ToLower()] = worker;
        }

        internal static void AddInformation(Func<string> worker) {
            _informationRetrievers.Add(worker);
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

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            e.Handled = true;
            switch (e.Key) {
                case KeyboardKey.BackSpace:
                    if (UserInput.Length == 0) {
                        return;
                    }
                    UserInput = UserInput[0..^1];
                    break;
                case KeyboardKey.Enter:
                    string trimmedInput = UserInput.Trim();
                    if (trimmedInput.Length != 0) {
                        var data = UserInput.Split(null);
                        if (data.Length > 0) {
                            string cmd = data[0].ToLower();
                            if (_commands.ContainsKey(cmd)) {
                                _commands[cmd].Invoke(data[1..]);
                            }
                        }
                        if (_pastCommands.LastOrDefault() != UserInput) {
                            _pastCommands.Add(UserInput);
                        }
                        _commandPtr = _pastCommands.Count;
                    }
                    UserInput = "";
                    break;
                case KeyboardKey.Space:
                    UserInput += " ";
                    break;
                case KeyboardKey.Up:
                    _commandPtr -= 1;

                    if (_commandPtr < 0) {
                        _commandPtr = 0;
                    }

                    if (_commandPtr == _pastCommands.Count) {
                        UserInput = "";
                    } else {
                        UserInput = _pastCommands[_commandPtr];
                    }
                    break;
                case KeyboardKey.Down:
                    _commandPtr += 1;

                    if (_commandPtr > _pastCommands.Count) {
                        _commandPtr = _pastCommands.Count;
                    }

                    if (_commandPtr == _pastCommands.Count) {
                        UserInput = "";
                    } else {
                        UserInput = _pastCommands[_commandPtr];
                    }
                    break;
                default:
                    UserInput += e.Key.GetKeyContent(e.ShiftDown);
                    break;
            }
        }
    }
}
