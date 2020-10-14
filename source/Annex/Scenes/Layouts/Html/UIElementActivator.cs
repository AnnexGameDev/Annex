using Annex.Scenes.Components;
using System;

namespace Annex.Scenes.Layouts.Html
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
