using Annex.Core.Assets;
using Annex.Core.Graphics;
using Annex.Core.Input;
using Annex.Core.Input.Platforms;
using Annex.Core.Networking;
using Annex.Core.Scenes;
using Annex.Core.Scenes.Elements;
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
    public void Launch<TStartingScene>() where TStartingScene : IScene
    {
        var sceneService = this.Container.Resolve<ISceneService>();
        sceneService!.LoadScene<TStartingScene>();
    }

    protected override void RegisterTypes(IContainer container)
    {
        base.RegisterTypes(container);

        container.RegisterAggregate<IUIElementTypeResolver, AnnexUIElementTypeResolver>();
        container.RegisterSingleton<IUIElementTypeResolverService, UIElementTypeResolverService>();
        container.Register<IHtmlSceneLoader, HtmlSceneLoader>();
        container.RegisterSingleton<IInputService, InputService>();
        container.RegisterSingleton<ITimeService, StopwatchTimeService>();
        container.RegisterSingleton<ISceneService, SceneService>();
        container.RegisterSingleton<IGraphicsService, GraphicsService>();
        container.RegisterSingleton<IPacketHandlerService, PacketHandlerService>();

        container.RegisterAssetGroup(KnownAssetGroups.TextureGroupId);
        container.RegisterAssetGroup(KnownAssetGroups.FontGroupId);
        container.RegisterAssetGroup(KnownAssetGroups.SceneDataGroupId);
        container.RegisterSingleton<IAssetService, AssetService>();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            container.Register<IPlatformKeyboardService, WindowsKeyboardService>();
        }
    }
}
