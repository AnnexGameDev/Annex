#if WINDOWS
using Annex.Core.Graphics.Windows;
using System.Runtime.InteropServices;

namespace Annex.Core.Platforms.Windows;

internal class WindowsScreenService : IPlatformScreenService
{
    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(SystemMetric smIndex);

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

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

    public void MaximizeWindow(IWindow window)
    {
        ShowWindowAsync(window.SystemHandle, (int)SW.SW_MAXIMIZE);
        window.IsVisible = true;
    }
}
#endif