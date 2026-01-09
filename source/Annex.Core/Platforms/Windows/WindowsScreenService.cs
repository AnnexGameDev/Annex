#if WINDOWS
using System.Runtime.InteropServices;

namespace Annex.Core.Platforms.Windows;

internal class WindowsScreenService : IPlatformScreenService
{
    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(SystemMetric smIndex);

    public (int Width, int Height) GetPrimaryScreenSize()
    {
        int width = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        int height = GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        return new(width, height);
    }

    public (int Width, int Height) GetPrimaryScreenContentSize()
    {
        int width = GetSystemMetrics(SystemMetric.SM_CXFULLSCREEN);
        int height = GetSystemMetrics(SystemMetric.SM_CYFULLSCREEN);
        return new(width, height);
    }
}
#endif