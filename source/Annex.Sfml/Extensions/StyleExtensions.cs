using Annex_Old.Core.Graphics.Windows;
using SFML.Window;

namespace Annex_Old.Sfml.Extensions
{
    internal static class StyleExtensions
    {
        public static Styles ToSfmlStyle(this WindowStyle style) {
            return style switch {
                WindowStyle.Close => Styles.Close,
                WindowStyle.Default => Styles.Default,
                WindowStyle.Fullscreen => Styles.Fullscreen,
                WindowStyle.None => Styles.None,
                WindowStyle.Titlebar => Styles.Titlebar,
                _ => throw new InvalidOperationException($"No Styles equivalent exists for {style}")
            };
        }
    }
}