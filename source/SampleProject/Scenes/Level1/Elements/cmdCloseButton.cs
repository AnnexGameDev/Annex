namespace SampleProject.Scenes.Level1.Elements
{
    public class cmdCloseButton : Button
    {
        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            base.HandleMouseButtonPressed(e);

            if (e.Handled) return;

            Game.Terminate();
        }
    }
}
