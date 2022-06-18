using Annex.Core.Scenes.Components;
using System.Reflection;

namespace Annex.Core.Scenes.Layouts
{
    internal class UIElementFactory
    {
        private readonly static Dictionary<string, Type> _uiElementTypes = new();

        static UIElementFactory() {
            RegisterTypes(ExtractTypes(Assembly.GetEntryAssembly()!));
            RegisterTypes(ExtractTypes(Assembly.GetExecutingAssembly()));
        }

        private static void RegisterTypes(IEnumerable<Type> enumerable) {
            foreach (var type in enumerable) {
                _uiElementTypes.Add(type.Name.ToLower(), type);
            }
        }

        private static IEnumerable<Type> ExtractTypes(Assembly assembly) {
            return assembly.GetTypes().Where(type => type.IsAssignableTo(typeof(IUIElement)) && !type.IsAbstract && type.IsClass);
        }

        public static IUIElement CreateInstance(string name) {
            var type = _uiElementTypes[name.ToLower()];

            var constructor = type.GetConstructors().First(c => c.GetParameters().All(p => p.IsOptional));
            var args = constructor.GetParameters().Select(c => c.RawDefaultValue).ToArray();
            return (IUIElement)constructor.Invoke(args);
        }
    }
}
