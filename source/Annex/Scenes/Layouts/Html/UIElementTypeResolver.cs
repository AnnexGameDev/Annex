using System;
using System.Reflection;

namespace Annex.Scenes.Layouts.Html
{
    public class UIElementTypeResolver
    {
        private readonly string _assemblyName;
        private readonly string _bindingNamespace;

        public UIElementTypeResolver(Assembly assembly, string bindingNamespace) {
            Debug.Assert(assembly.GetName().Name != null, $"{nameof(UIElementTypeResolver)} cannot resolve types in a null named assembly");
            this._assemblyName = assembly.GetName().Name!;
            this._bindingNamespace = bindingNamespace;
        }

        public Type Resolve(string className) {
            string fullType = $"{this._bindingNamespace}.{className}, {this._assemblyName}";
            var type = Type.GetType(fullType);
            Debug.ErrorIf(type == null, $"Unable to resolve type for {fullType}");
            return type!;

        }
    }
}
