---
layout: default
title: Setting Up
nav_order: 2
parent: v0.0.2
search_exclude: true
---

**Careful:** This page was for meant for an older version of Annex
{: .deprecated }

# Installing Annex Through NuGet
[How to install packages with NuGet](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio)

[Annex.Game package link](https://www.nuget.org/packages/Annex.Game/)

# Main
Create a new .Net Core project and reference Annex the above method.
In your main, instantiate a new AnnexGame object and call the Start function to let Annex start running your game. It is important to specify the starting scene when calling the Start function. For more information about scenes, read [here](https://github.com/MatthewChrobak/Annex/wiki/Scenes).

```cs
using Annex;

public class Game
{
    private static void Main() {
        new AnnexGame().Start<StartingScene>();
    }
}
```

# External Resources
Most games use external resources. Game music and textures are usually not embedded into your application but are files stored somewhere (usually) in the same folder as the game application. With Annex, we'll have all these resources (audio, textures, fonts) stored in a Resources/ folder in the same place as your solution file (.sln). Whenever you're adding new resources, or modifying old ones, use the Resources/ folder located in your solution directory.

Your game can be built under different configurations. Generally, there are two: debug, and release. Applications built in the Debug configuration (debug mode) are usually a little bit slower than the release versions. The resulting .exe file isn't optimized for speed, but makes debugging easier. When testing out your game, debug mode is fine to use. Once your game is ready to be shipped out to the public, it would be a good idea to build your game in release mode.

When you build your application, there's a few places it will be built. If our solution is called GameSolution and our project is called GameProject, you can find your builds in these folders.

* GameSolution/GameProject/bin/Debug/
* GameSolution/GameProject/bin/Release/

The architecture target for these builds are labled as Any CPU. If you modify your build configuration to optimize for x64 or x86 architecture, you can find your builds in any of these folders.

* GameSolution/GameProject/bin/x86/Debug/
* GameSolution/GameProject/bin/x86/Release/
* GameSolution/GameProject/bin/x32/Debug/
* GameSolution/GameProject/bin/x32/Release/

When your game is built in debug mode, it's assumed that you're going to be adding and modifying assets quite a bit. So when you run a game in debug mode, all the resources from the solution folder will be copied into the build folder.

* Audio files (.flac) should be stored in Resources/Audio/
* Texture files (.png) should be stored in Resources/Textures/
* Font files (.ttf) should be stored in Resources/Fonts/