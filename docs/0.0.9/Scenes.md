---
layout: default
title: Scenes
nav_order: 0
parent: v0.0.9
# search_exclude: true
---

# Scene State
Annex defines a game state as a _scene_.

![Example state changing](https://i.imgur.com/aCfsysf.png)

A scene is responsible for storing and representing its data, and for handling user interaction with the scene.


```CSharp
var scenes = ServiceProvider.SceneService;
```

## Creating a scene
Scenes are defined by creating a class that inherits from the ```Scene``` class.

**Note:** In order for Annex to maintain scene instances, all scenes need to have a default constructor in order to be loaded.
{: .note }
```cs
public class MainMenu : Scene 
{
}
```

## Loading scenes
Scenes can be loaded by using the ```LoadScene``` method.
If the scene specified has been visited before, the previous instance will be loaded instead of creating a new one. To force the creation of a new instance of the scene, use the ```LoadNewScene``` method.
```cs
scenes.LoadScene<MainMenu>();
scenes.LoadNewScene<MainMenu>();
```

Retrieving the current state can be done through the scene service.
```cs
var mainMenu = scenes.CurrentScene as MainMenu;
```

## Closing the game
Safely terminating an Annex game is done by loading the ```GameClosing``` scene, which can be done by calling ```LoadGameClosingScene```.

```cs
scenes.LoadGameClosingScene();
```

## Starting your game
Starting a new Annex game requires specifying the starting state. 

```cs
private static void Main() {
    AnnexGame.Initialize();
    AnnexGame.Start<MainMenu>();
}
```

## User Input
Each scene is responsible for handling the user-input while on the scene. An input handler is defined for each type of input event. 

Annex currently supports the Mouse, Keyboard, and Game Controller events listed below.

**Note:** It is important to call ```base.Handle(e)``` for any overridden event handler if you want the event to be propagated to child UI elements.
{: .note }
```cs
public override void HandleCloseButtonPressed() {
    // When the window's x button is closed.
}

// For mouse events:
// e.mouseX/Y is the actual x/y distance from the top left of the user's canvas.
// e.worldX/Y is the x/y coordinate of where the user clicked adjusted to where 
//            the user's game-camera is positioned.
public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
}

public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e) {
}

public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
}
    
public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e) {
}

public override void HandleJoystickMoved(JoystickMovedEvent e) {
}

public override void HandleJoystickButtonPressed(JoystickButtonPressedEvent e) {
}

public override void HandleJoystickButtonReleased(JoystickButtonReleasedEvent e) {
}

public override void HandleJoystickDisconnected(JoystickDisconnectedEvent e) {
}

public override void HandleJoystickConnected(JoystickConnectedEvent e) {
}

```

## User Interface
Each scene has its own user interface. Annex currently offers basic user controls: Labels, Buttons, and Textboxes. Handling user input on UI elements can be done in the same manner that user input in a scene is handled.

```cs
using Annex.Data;
using Annex.Graphics.Events;
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

    public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
    }
}

public class MainMenu : Scene
{
    public MainMenu() {
        this.AddChild(new EnterGameButton());
    }
}
```