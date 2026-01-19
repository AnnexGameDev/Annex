using Annex.Core.Graphics.Windows;

namespace Annex.Core.Platforms;

public interface IPlatformScreenService
{
    (int Width, int Height) GetPrimaryScreenSize();
    (int Width, int Height) GetPrimaryScreenContentSize();
    void MaximizeWindow(IWindow window);
}
