﻿using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Scenes.Components
{
    public interface ILabel : IUIElement
    {
        string Text { get; set; }
        string Font { get; set; }
        uint FontSize { get; set; }
        RGBA FontColor { get; set; }
        HorizontalAlignment HorizontalTextAlignment { get; set; }
        VerticalAlignment VerticalTextAlignment { get; set; }
        IVector2<float> TextPositionOffset { get; set; }
    }
}
