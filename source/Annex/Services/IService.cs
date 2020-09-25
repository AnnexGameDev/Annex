using Annex.Assets;
using System.Collections.Generic;

namespace Annex.Services
{
    public interface IService
    {
        void Destroy();
        IEnumerable<IAssetManager> GetAssetManagers();
    }
}
