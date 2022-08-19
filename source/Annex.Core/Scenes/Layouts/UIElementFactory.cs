using Annex.Core.Scenes.Components;
using Scaffold.DependencyInjection;
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

        public static IUIElement CreateInstance(string name, IContainer container) {
            var type = _uiElementTypes[name.ToLower()];

            return (IUIElement)container.Resolve(type);
        }
    }
}
