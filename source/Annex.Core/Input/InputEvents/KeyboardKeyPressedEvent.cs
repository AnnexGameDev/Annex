namespace Annex.Core.Input.InputEvents
{
    public class KeyboardKeyPressedEvent : KeyboardEvent
    {
        public string LiteralContent { get; }
        public bool IsShiftPressed { get; }
        public bool IsCapsLockEnabled { get; }
        public bool IsControlPressed { get; }

        public KeyboardKeyPressedEvent(KeyboardKey key, bool shiftDown, bool capsLock, bool isControlPressed) : base(key) {
            if (key >= KeyboardKey.A && key <= KeyboardKey.Z)
            {
                this.LiteralContent = key.ToString();

                if (!shiftDown && !capsLock)
                {
                    this.LiteralContent = this.LiteralContent.ToLower();
                }
                return;
            }

            this.LiteralContent = HandleParticularCase(key, shiftDown);
            IsShiftPressed = shiftDown;
            IsCapsLockEnabled = capsLock;
            IsControlPressed = isControlPressed;
        }

        private string HandleParticularCase(KeyboardKey key, bool shiftDown) {
            return key switch
            {
                KeyboardKey.Tilde => shiftDown ? "~" : "`",
                KeyboardKey.Num1 => shiftDown ? "!" : "1",
                KeyboardKey.Num2 => shiftDown ? "@" : "2",
                KeyboardKey.Num3 => shiftDown ? "#" : "3",
                KeyboardKey.Num4 => shiftDown ? "$" : "4",
                KeyboardKey.Num5 => shiftDown ? "%" : "5",
                KeyboardKey.Num6 => shiftDown ? "^" : "6",
                KeyboardKey.Num7 => shiftDown ? "&" : "7",
                KeyboardKey.Num8 => shiftDown ? "*" : "8",
                KeyboardKey.Num9 => shiftDown ? "(" : "9",
                KeyboardKey.Num0 => shiftDown ? ")" : "0",
                KeyboardKey.Dash => shiftDown ? "_" : "-",
                KeyboardKey.Equal => shiftDown ? "+" : "=",
                KeyboardKey.LBracket => shiftDown ? "{" : "[",
                KeyboardKey.RBracket => shiftDown ? "}" : "]",
                KeyboardKey.SemiColon => shiftDown ? ":" : ";",
                KeyboardKey.Quote => shiftDown ? "\"" : "'",
                KeyboardKey.Comma => shiftDown ? "<" : ",",
                KeyboardKey.Period => shiftDown ? ">" : ".",
                KeyboardKey.Slash => shiftDown ? "?" : "/",
                KeyboardKey.Backslash => shiftDown ? "|" : "\\",
                KeyboardKey.Numpad0 => "0",
                KeyboardKey.Numpad1 => "1",
                KeyboardKey.Numpad2 => "2",
                KeyboardKey.Numpad3 => "3",
                KeyboardKey.Numpad4 => "4",
                KeyboardKey.Numpad5 => "5",
                KeyboardKey.Numpad6 => "6",
                KeyboardKey.Numpad7 => "7",
                KeyboardKey.Numpad8 => "8",
                KeyboardKey.Numpad9 => "9",
                KeyboardKey.Subtract => "-",
                KeyboardKey.Add => "+",
                KeyboardKey.Divide => "/",
                KeyboardKey.Multiply => "*",
                KeyboardKey.Space => " ",
                KeyboardKey.Tab => "\t",
                _ => string.Empty
            };
        }
    }
}