﻿using Annex.Core.Data;

namespace Annex.Core.Graphics.Windows
{
    public interface IWindow
    {
        Vector2ui WindowResolution { get; }
        Vector2ui WindowSize { get; }
        Vector2i WindowPosition { get; }
        WindowStyle WindowStyle { get; set; }
        string Title { get; set; }
        bool IsVisible { get; set; }
    }
}