#nullable enable
using Annex.Resources;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static ResourceManagerRegistry ResourceManagerRegistry => Locate<ResourceManagerRegistry>() ?? Provide<ResourceManagerRegistry>(new ResourceManagerRegistry());
    }
}
