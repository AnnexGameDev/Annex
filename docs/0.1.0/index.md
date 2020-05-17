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

# Main
Create a new .Net Core project and reference Annex the above method.
In your main, all service providers and game events are created upon calling ``Initialize```. When calling ```Start```, that starting scene is specified. For more information about scenes, read [here](scenes).

```cs
using Annex;

public class Game
{
    private static void Main() {
        AnnexGame.Initialize();
        AnnexGame.Start<StartingScene>();
    }
}
```

