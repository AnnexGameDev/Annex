using System;

namespace Annex.Services
{
    public interface IServiceContainer : IDisposable
    {
        void SetService(Type serviceType, object service);
        object? GetService(Type serviceType);
        void RemoveService(Type serviceType);
    }
}
