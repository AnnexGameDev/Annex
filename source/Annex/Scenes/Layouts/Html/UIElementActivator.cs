using Annex_Old.Scenes.Components;
using System;

namespace Annex_Old.Scenes.Layouts.Html
{
    public class UIElementActivator
    {
        private Type[] IdConstructorParameters = new Type[] { typeof(string) };

        public UIElement CreateInstance(Type type, string? id) {

            if (type.GetConstructor(IdConstructorParameters) != null) {
                return (UIElement)Activator.CreateInstance(type, id ?? Guid.NewGuid().ToString())!;
            } else {
                return (UIElement)Activator.CreateInstance(type)!;
            }
        }
    }
}
