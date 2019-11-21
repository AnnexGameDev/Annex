namespace Annex.Scenes
{
    public static class Extensions
    {
        public static string GetKeyContent(this KeyboardKey key, bool shift) {

            // Letters
            if ((int)key >= (int)KeyboardKey.A && (int)key <= (int)KeyboardKey.Z) {
                if (shift) {
                    return key.ToString();
                }
                return key.ToString().ToLower();
            }

            switch (key) {
                case KeyboardKey.Equal:
                    return shift ? "+" : "=";
                case KeyboardKey.Add:
                    return "+";
                case KeyboardKey.Backslash:
                    return shift ? "|" : "\\";
                case KeyboardKey.Comma:
                    return shift ? "<" : ",";
                case KeyboardKey.Dash:
                    return shift ? "_" : "-";
                case KeyboardKey.Divide:
                    return "/";
                case KeyboardKey.LBracket:
                    return shift ? "{" : "[";
                case KeyboardKey.Multiply:
                    return "*";
                case KeyboardKey.Num0:
                    return shift ? ")" : "0";
                case KeyboardKey.Num1:
                    return shift ? "!" : "1";
                case KeyboardKey.Num2:
                    return shift ? "@" : "2";
                case KeyboardKey.Num3:
                    return shift ? "#" : "3";
                case KeyboardKey.Num4:
                    return shift ? "$" : "4";
                case KeyboardKey.Num5:
                    return shift ? "%" : "5";
                case KeyboardKey.Num6:
                    return shift ? "^" : "6";
                case KeyboardKey.Num7:
                    return shift ? "&" : "7";
                case KeyboardKey.Num8:
                    return shift ? "*" : "8";
                case KeyboardKey.Num9:
                    return shift ? "(" : "9";
                case KeyboardKey.Period:
                    return shift ? ">" : ".";
                case KeyboardKey.Quote:
                    return shift ? "\"" : "'";
                case KeyboardKey.RBracket:
                    return shift ? "}" : "]";
                case KeyboardKey.Semicolon:
                    return shift ? ":" : ";";
                case KeyboardKey.Slash:
                    return shift ? "?" : "/";
                case KeyboardKey.Subtract:
                    return "-";
                case KeyboardKey.Tilde:
                    return shift ? "~" : "`";
                default:
                    return string.Empty;
            }
        }
    }
}
