using Annex.Core.Graphics;
using Annex.Core.Hardware;
using Annex.Core.Input;
using Annex.Core.Networking;
using Annex.Core.Platforms;
using Annex.Core.Platforms.Windows;
using Annex.Core.Scenes.Layouts;
using Annex.Core.Scenes.Layouts.Html;
using Annex.Core.Time;
using Scaffold;
using Scaffold.DependencyInjection;
using Scaffold.Extensions;
using System.Runtime.InteropServices;

namespace Annex.Core;

public abstract class AnnexApp : ScaffoldApp
{
    protected override void RegisterTypes(IContainer container)
    {
        base.RegisterTypes(container);

        container.RegisterAggregate<IUIElementTypeResolver, AnnexUIElementTypeResolver>();
        container.RegisterSingleton<IUIElementTypeResolverService, UIElementTypeResolverService>();
        container.Register<IHtmlSceneLoader, HtmlSceneLoader>();
        container.RegisterSingleton<ITimeService, StopwatchTimeService>();
        container.RegisterSingleton<IGraphicsService, GraphicsService>();
        container.RegisterSingleton<IPacketHandlerService, PacketHandlerService>();
        container.Register<IInputHandler, InputHandler>();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            container.Register<IPlatformKeyboardService, WindowsKeyboardService>();
            container.Register<IPlatformScreenService, WindowsScreenService>();
        }
    }
}
