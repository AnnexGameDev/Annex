# Game Window
Before talking about game graphics, it's important to understand its containing element which is the game window. The game window can be accessed through its singleton.

``` CSharp
var window = GameWindow.Singleton;
```

The game window contains an object which is the context to perform graphical operations (canvas). 

``` CSharp
var canvas = window.Canvas;
```

# Rendering Process
Rendering any text or images is done through the Draw function, which is a part of the canvas.
Its parameters are a _TextContext_ or _TextureContext_. Both of which are intermediate objects that contain information about **what** needs to be rendered, and **how**.

``` CSharp
canvas.Draw(ctx);
```

But you shouldn't call this function just _anywhere_ in your code. There's a certain process behind rendering a frame of your game, and if you don't call Draw() at the right moment, it won't appear at all.

## Render Order

There is a game event that gets generated in the Graphics priority which lets you know when its safe to start drawing. The game event take care of the necessary preparations, and then calls the Draw function in the current scene when it's ready. All of your draw calls should stem from the Draw function in the scene.

``` CSharp
public class Foo : Scene
{
    public override void Draw(ICanvas canvas) {
        // canvas.Draw(ctx);
    }
}
```

# Text / Texture Contexts
As mentioned before, a text or texture context is an intermediate object that describes what needs to be rendered, and how.

## Text Context
A text context specifies text that needs to be drawn to the screen. Not all these properties need to be specified, but you will find yourself using a few of them fairly often. Keep in mind that font files should be stored in the appropriate Resources/Fonts/ folder as .ttf files.

``` CSharp
var ctx = new TextContext("Text that has to be rendered.", "Font.ttf");

// always absolute positioning. By default, (0, 0).
ctx.RenderPosition = Vector.Create(10, 10);

// By default, top left positioning is used.
ctx.Alignment = new TextAlignment() {
    HorizontalAlignment = HorizontalAlignment.Left,
    VerticalAlignment = VerticalAlignment.Bottom,
    // Top, Left, Bottom, Right are all calculated based off of RenderPosition and Size.
    // bottom is Position.Y + Size.height
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

## Texture context
A texture context specifies an image that needs to be drawn to the screen. Not all these properties need to be specified, but you will find yourself using a few of them fairly often. Keep in mind that image files should be stored in the appropriate Resources/Textures/ folder as .png files.

``` CSharp
var ctx = new TextureContext("image.png");

// always absolute positioning. By default, (0, 0).
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

# Organizing Draw Calls
Ideally, each entity that can be drawn will be responsible for creating its own contexts and making appropriate draw calls.

``` CSharp
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
The camera is a data structure belonging to the canvas that is responsible for specifying the current view of the content in your scene. It specifies what area of the scene you're in (position), and how much of the region to show (size). You can retrieve the camera through the canvas.

``` CSharp
var camera = GameWindow.Singleton.Canvas.GetCamera();
```

The camera object supports various manipulations.

``` CSharp
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

``` CSharp
if (canvas.IsKeyDown(KeyboardKey.BackSlash)) { ... }
if (canvas.IsMouseButtonDown(MouseButton.Left)) { ... }
if (canvas.IsJoystickConnected(0)) { ... }
if (canvas.IsJoystickButtonPressed(JoystickButton.A)) { ... }
float delta = canvas.GetJoystickAxis(0, JoystickAxis.R);
```