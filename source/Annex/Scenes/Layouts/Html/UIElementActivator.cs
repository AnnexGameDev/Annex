using Annex.Scenes.Components;
using System;

namespace Annex.Scenes.Layouts.Html
{
    public class UIElementActivator
    {
        public UIElement CreateInstance(Type type, string? id) {
            if (id == null) {
                return (UIElement)Activator.CreateInstance(type)!;
            }
            return (UIElement)Activator.CreateInstance(type, id)!;
        }
    }
}
