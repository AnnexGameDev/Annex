---
layout: default
title: v0.1.0
nav_order: 0
has_children: true
has_toc: false
---

# Installing Annex Through NuGet
[How to install packages with NuGet](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio)

[Annex.Game package link](https://www.nuget.org/packages/Annex.Net/)

# Example
A minimal Annex game is implemented with the code shown below.

In your main, call ```AnnexGame.Initialize``` to initialize the necessary service providers. Calling ```AnnexGame.Start``` will start the game's event system, and show the canvas.

Note that ```Start``` takes in a starting scene, which is user-defined. For more information about scenes, read [here](scenes).
{: .note }

```cs
using Annex;
using Annex.Scenes.Components;

public class Game
{
    private static void Main() {
        AnnexGame.Initialize();
        AnnexGame.Start<StartingScene>();
    }
}

public class StartingScene : Scene
{

}
```

