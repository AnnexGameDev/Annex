Most games usually change their state pretty frequently. You start up your game, and see a loading screen followed by a main menu. You then hit "play now" and get thrown into some game world. Without realizing it, your game is switching between different states.

![Example state changing](https://i.imgur.com/aCfsysf.png)

In Annex, a scene is a game state. You can interact with the game scenes using the SceneManager singleton.

``` CSharp
var scenes = SceneManager.Singleton;
```

Scenes have various responsibilities, like exposing a user interface, handling user input, playing background music, and drawing game content on the screen.

## Creating a scene
You can create a scene by inheriting from the Scene class.
``` CSharp
using Annex.Scenes.Components;

public class MainMenu : Scene 
{

}
```

## Changing scenes
You can change the scene your game is currently in by calling the LoadScene method.
``` CSharp
var scenes = SceneManager.Singleton;
scenes.LoadScene<MainMenu>();
```

Likewise, you can also retrieve the current scene by using the CurrentScene accessor.
``` CSharp
var scenes = SceneManager.Singleton;
var mainMenu = scenes.CurrentScene as MainMenu;
```

## Closing the game
One of the scenes defined by Annex is the 'GameClosing' scene. Once Annex loads the GameClosing scene, game events will stop running and your game will close.

``` CSharp
SceneManager.Singleton.LoadScene<GameClosing>();
```

## Starting your game
In order to start your game, Annex requires that you specify the initial scene to use on startup.

``` CSharp
private static void Main() {
    var game = new AnnexGame();
    game.Start<MainMenu>();
}
```

## User Input
As mentioned before, user input is another one of the responsibilities of the scene. You can get user input by overriding the scene's input handlers.
``` CSharp
using Annex.Scenes;
using Annex.Scenes.Components;

public class MainMenu : Scene
{
    // For mouse events,
    // e.mouseX/Y is the actual x/y distance from the top left of your window.
    // e.worldX/Y is the x/y coordinate of where you clicked adjusted to where your game-camera is positioned.

    public override void HandleCloseButtonPressed() {
        // When the window's x button is closed.
    }

    public virtual void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
    
    }

    public virtual void HandleMouseButtonReleased(MouseButtonReleasedEvent e) {

    }

    public virtual void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {

    }
        
    public virtual void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e) {

    }

    public virtual void HandleJoystickMoved(JoystickMovedEvent e) {

    }

    public virtual void HandleJoystickButtonPressed(JoystickButtonPressedEvent e) {

    }

    public virtual void HandleJoystickButtonReleased(JoystickButtonReleasedEvent e) {

    }

    public virtual void HandleJoystickDisconnected(JoystickDisconnectedEvent e) {

    }

    public virtual void HandleJoystickConnected(JoystickConnectedEvent e) {

    }
}

```

## User Interface
One of the primary responsibilities of a scene is the user interface that it exposes to the user. Annex provides a few basic controls for you to use.

- Label
- Button
- Textbox
- Checkbox (planned)

You can build on these fundamental controls, or create your own, and add them to your scene.

``` CSharp
using Annex.Data;
using Annex.Scenes.Components;

public class EnterGameButton : Button
{
    public EnterGameButton() {
        this.Position.Set(100, 100);
        this.Size.Set(100, 25);
        this.ImageTextureName.Set("gui/buttons/simplebutton.png");
        this.Caption.Set("Enter Game");
        this.Font.Set("Open Sans.ttf");
        this.RenderText.FontColor = RGBA.Black;
        this.RenderText.FontSize = 16;
    }
}

public class MainMenu : Scene
{
    public MainMenu() {
        var scenes = SceneManager.Singleton;

        this.AddChild(new EnterGameButton());
    }
}
```

## Custom controls
If you're interested in making your own custom control, inherit from the UIElement class. It is highly recommended to understand how the common controls provided by Annex work before making your own controls from scratch.
``` CSharp
public class ListBox : UIElement {

}
```