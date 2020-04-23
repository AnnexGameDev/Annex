---
layout: default
title: Data
nav_order: 2
parent: v0.0.2
grand_parent: Annex Home
# search_exclude: true
---

Shared data structures are wrappers around primitive data types which help different objects in your game look at the same primitive data, even when that primitive data changes.

The goal of shared data structures is to share values across multiple objects.

```cs
public class Player
{
    public string Name;
}

public static void Main() {
    var player1 = new Player();
    var player2 = new Player();

    player1.Name = "Bob";
    player2.Name = player1.Name;
    player1.Name = "Alice";

    Console.WriteLine(player2.Name); // Prints Bob
    Console.WriteLine(player1.Name); // Prints Alice
}
```

To help share string values between different objects, Annex introduces the shared string.

```cs
using Annex.Data.Shared;

public class Player
{
    public String Name;
}

public static void Main() {
    var player1 = new Player();
    var player2 = new Player();

    player1.Name = "Bob";
    player2.Name = player1.Name;
    player1.Name.Set("Alice");

    Console.WriteLine(player2.Name); // Prints Alice
    Console.WriteLine(player1.Name); // Prints Alice
}
```
There is a lot of use for shared data types in graphical components when attributes can change dynamically. For example, if you're rendering a player, where you render his sprite depends on where the player is.

```cs
public class Player : IDrawableObject
{
    private readonly TextureContext _sprite;

    // Put the player at 0,0
    public Vector Position = Vector.Create();

    public Player() {
        this._sprite = new TextureContext("sprite.png") {
            RenderSize = Vector.Create(100, 100),
            Position = this.Position
        };
    }

    public void Draw(ICanvas canvas) {
        // Without shared vectors, we need to manually update the position of the 
        // player each time we render the sprite. Otherwise, the sprite
        // will not appear to move.
        // Since Annex uses shared vectors, the line below can be removed.
        this._sprite.Position = this.Position;

        canvas.Draw(this._sprite);
    }
}
```

## String
Defines a string that can be shared across objects.

Example: Rendering a player name above their head.
```cs
using Annex.Data.Shared;

public class Player : IDrawableObject
{
    public readonly String PlayerName;
    private readonly String PlayerNameFont;

    public readonly Vector Position;
    
    private readonly TextContext _hoverName;

    public Player() {
        this.Position = Vector.Create();
        this.PlayerName = new String("Alex");
        this.PlayerNameFont = new String("Open Sans.ttf");

        this._hoverName = new TextContext(this.PlayerName, this.PlayerNameFont) {
            RenderPosition = this.Position,
            FontColor = RGBA.White,
            FontSize = 14
        };
    }

    // Since the PlayerName field is a shared string, the game will always render whatever
    // the player name is, even if its value has been changed at any RenderPosition.
    public void Draw(ICanvas canvas) {
        canvas.Draw(this._hoverName);
    }
}
```

## Int
A wrapper around an int value.

## Float
A wrapper around a float value.

## Vector
Defines a two-dimentional vector that can be shared across objects. Vectors must be created using the static Create method.

## OffsetVector
A type of shared vector which applies an offset (which can also be a shared vector) to the base vector.

> OffsetVector = BaseVector + Offset

## ScalingVector
A type of shared vector which applies a scale (which can also be a shared vector) to the base vector.

> ScalingVector = BaseVector * Scale