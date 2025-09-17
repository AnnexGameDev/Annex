using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Graphics.PlatformTargets;
using Annex.Sfml.Graphics.Windows;
using Scaffold.DependencyInjection;
using Scaffold.Extensions;
using SFML.Graphics;

namespace Annex.Sfml.Graphics;

public class SfmlGraphicsEngine : IGraphicsEngine
{
    private readonly IContainer _container;
    private readonly IPlatformTargetFactory _platformTargetFactory;

    public SfmlGraphicsEngine(IContainer container)
    {
        _container = container;
        _container.Register<IPlatformTargetFactory, PlatformTargetFactory>();
        _container.RegisterAggregate<IPlatformTargetCreator, TextPlatformTargetCreator>();
        _container.RegisterAggregate<IPlatformTargetCreator, TexturePlatformTargetCreator>();
        _container.RegisterAggregate<IPlatformTargetCreator, SpritesheetPlatformTargetCreator>();
        _container.RegisterAggregate<IPlatformTargetCreator, SolidRectanglePlatformTargetCreator>();
        _container.RegisterAggregate<IPlatformTargetCreator, BatchTexturePlatformTargetCreator>();
        _container.Register<ICameraCache, CameraCache>();

        _platformTargetFactory = _container.Resolve<IPlatformTargetFactory>();
    }

    public Core.Data.FloatRect GetTextBounds(TextContext textContext, bool forceContextUpdate)
    {
        if (forceContextUpdate)
        {
            _platformTargetFactory.GetPlatformTarget(textContext);
        }
        if (textContext.PlatformTarget is TextPlatformTarget textPlatformTarget)
        {
            return textPlatformTarget.GetTextBounds();
        }
        throw new InvalidOperationException($"Unable to transform textContext to text platform target");
    }

    public float GetCharacterX(TextContext textContext, int index, bool forceContextUpdate)
    {
        if (forceContextUpdate)
        {
            _platformTargetFactory.GetPlatformTarget(textContext);
        }

        if (textContext.PlatformTarget is TextPlatformTarget platformTarget)
        {
            return platformTarget.GetCharacterX(index);
        }
        throw new InvalidOperationException($"Unable to transform textContext to text platform target");
    }

    public IWindow CreateWindow(string title, uint width, uint height, WindowStyle windowStyle)
    {
        return new SfmlWindow(_container, title, width, height, windowStyle);
    }

    public static object TextureLoadingStrategy(string assetId) => new Texture(assetId);
    public static object FontLoadingStrategy(string assetId) => new Font(assetId);
}