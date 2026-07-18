using Annex.Core.Data;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;
using Scaffold.DependencyInjection;

namespace Annex.Core.Scenes.Elements;

public class PasswordBox : Textbox, IPasswordBox
{
    public char PasswordChar { get; set; } = '*';

    public PasswordBox(IContainer container, UIElementCreationArgs? args) : base(container, args)
    {
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        // Basically just a hack. Swap out the text each render so the logic still holds, but we prevent 
        // the actual text from being read
        string oldText = this.Text;
        this.Text = new string(this.PasswordChar, this.Text.Length);
        base.DrawInternal(window, timeDelta);
        this.Text = oldText;
    }

    public override void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent)
    {

        // Prevent copying/pasting
        if (mouseButtonReleasedEvent.Button == Input.MouseButton.Right)
        {
            return;
        }

        base.OnMouseButtonReleased(mouseButtonReleasedEvent);
    }

    public override void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent)
    {

        // Prevent copying/pasting
        if (PlatformKeyboardService.IsControlPressed() &&
            (keyboardKeyPressedEvent.Key == Input.KeyboardKey.C || keyboardKeyPressedEvent.Key == Input.KeyboardKey.X))
        {
            return;
        }

        base.OnKeyboardKeyPressed(keyboardKeyPressedEvent);
    }
}
