---
layout: default
title: Graphics
nav_order: 0
parent: v0.0.9
# search_exclude: true
---

# Canvas
The canvas is the object responsible for the game window, and performing graphical operations on it. It is accessible through the service provider.

```cs
var canvas = ServiceProvider.Canvas;
```

# Rendering Process
Rendering any text or images is done through the ```Draw``` function, which takes in a context. The context is an intermediate object that contain information about **what** needs to be rendered, and **how**.

```cs
canvas.Draw(ctx);
```

**Note**: Draw calls shouldn't be called arbitrarily. Drawing a frame of a game requires setup, ordering, and cleanup. If a draw isn't called at the right moment, it won't appear at all.
{: .note} 

## Render Order
A canvas implementation will usually create a game event which is responsible for initiating the process of drawing a frame. Once ready, the ```Draw``` method in your current scene will run.

```cs
public class Foo : Scene
{
    public override void Draw(ICanvas canvas) {
        // TODO: draw game
    }
}
```

## Text Context
A text context specifies text that needs to be drawn to the screen.

```cs
var ctx = new TextContext("Text that has to be rendered.", "Font.ttf");

// Always absolute positioning. By default, (0, 0).
ctx.RenderPosition = Vector.Create(10, 10);

// By default, top left positioning is used.
ctx.Alignment = new TextAlignment() {
    HorizontalAlignment = HorizontalAlignment.Left,
    VerticalAlignment = VerticalAlignment.Bottom,
    // Top, Left, Bottom, Right are all calculated based off of RenderPosition and Size.
    // Bottom is Position.Y + Size.height
    // Top is Position.Y
    // Left is Position.X
    // Right is Position.X + Size.Width
    Size = Vector.Create(100, 100) 
};

// By default, 12 is used.
ctx.FontSize = 24;

// By default, white is used.
ctx.FontColor = RGBA.Blue;

// By default, 0 and white are used.
ctx.BorderThickness = 1;
ctx.BorderColor = RGBA.Red;
```

## Texture Context
A texture context specifies an image that needs to be drawn to the screen.

```cs
var ctx = new TextureContext("image.png");

// Always absolute positioning. By default, (0, 0).
ctx.RenderPosition = Vector.Create(100, 100);

// How big the final render should be on screen.
ctx.RenderSize = Vector.Create(100, 100);

// What sub-section of the source image should be used to render.
// This specifies a 50x100 pixel rectangle starting from (0, 10) 
// taken from the source image.
ctx.SourceSurfaceRect = new IntRect(0, 10, 50, 100);

// Apply a color tint to your render.
ctx.RenderColor = RGBA.Red;

// Rotate 180 degrees relative to the top left of the render position.
ctx.Rotation = 180;
ctx.RelativeRotationOrigin = Vector.Create(0, 0);
```

## Solid Rectangle Context
A solid rectangle context specifies a rectangular shape with a solid color fill that needs to be drawn to the screen.

```cs
var ctx = new SolidRectangleContext(RGBA.Red);

// Always absolute positioning. By default, (0, 0).
ctx.RenderPosition = Vector.Create(100, 100);

// How big the final render should be on screen.
ctx.RenderSize = Vector.Create(100, 100);

// Draws a 1.5pixel border around the rectangle shape. 
// By default, RenderBorderColor is null and RenderBorderSize is 1.
ctx.RenderBorderColor = RGBA.Black;
ctx.RenderBorderSize = 1.5f;

```

## Sprite Sheet Context
A sprite sheet context is an extension of the TextureContext, used typically for animations.

**Note:** SpriteSheetContext handles setting the SubTextureRect for you so you don't have to.
{: .note }

![Spritesheet](https://raw.githubusercontent.com/MatthewChrobak/Annex/master/source/Resources/textures/player.png)

```cs
int numRows = 4;
int numColumns = 4;
var spritesheet = new SpriteSheetContext("spritesheet.png", numRows, numColumns);

// If the spritesheet is a subsection of the texture, SpriteSheetContext can still be used.
var spritesheet = new SpriteSheetContext("spritesheet.png", 4, 4, 0, 0, 96, 96);
```

# Organizing Draw Calls
Each entity that is drawn should be responsible for creating its own contexts, and making 

```cs
public class Player : IDrawableObject
{
    private readonly TextureContext _textureContext;

    public Player() {
        this._textureContext = new TextureContext("Player.png");
    }

    public void Draw(ICanvas canvas) {
        canvas.Draw(this._textureContext);
    }
}

public class ExampleScene : Scene
{
    private Player _player = new Player();

    public override void DrawScene(ICanvas canvas) {
        this._player.Draw(canvas);
    }
}
```

# Camera
Accessing the game camera can be done through the canvas.

```cs
var camera = ServiceProvider.Canvas.GetCamera();
```

The camera object supports various manipulations.

```csharp
// Should respect the resolution ratio. 
// After resizing, the camera re-centers based on the current location.
camera.Resize(GameWindow.RESOLUTION_WIDTH / 2, GameWindow.RESOLUTION_HEIGHT / 2);

// Moves the camera to the specified centerpoint location.
camera.SetPosition(0, 0);

// Given a shared vector, the camera will follow it.
camera.Follow(vector);

// Zoom in and out by a specified delta 
// (currentZoom = currentZoom +- delta)
camera.ZoomIn(0.1f);
camera.ZoomOut(0.1f);
```

## Hardware Probing
You can use the canvas to check the current state of IO devices like the mouse, keyboard, or controllers.

```cs
if (canvas.IsKeyDown(KeyboardKey.BackSlash)) { ... }
if (canvas.IsMouseButtonDown(MouseButton.Left)) { ... }
if (canvas.IsJoystickConnected(0)) { ... }
if (canvas.IsJoystickButtonPressed(JoystickButton.A)) { ... }
float delta = canvas.GetJoystickAxis(0, JoystickAxis.R);
```