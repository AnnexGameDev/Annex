﻿using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Graphics.PlatformTargets;
using Annex.Sfml.Graphics.Windows;
using Scaffold.DependencyInjection;

namespace Annex.Sfml.Graphics
{
    public class SfmlGraphicsEngine : IGraphicsEngine
    {
        private readonly IContainer _container;
        private readonly IPlatformTargetFactory _platformTargetFactory;

        public IWindow CreateWindow() => this._container.Resolve<SfmlWindow>();

        public SfmlGraphicsEngine(IContainer container) {
            this._container = container;
            this._container.Register<SfmlWindow>();
            this._container.Register<IPlatformTargetFactory, PlatformTargetFactory>();
            this._container.RegisterSingleton<ITextureCache, TextureCache>();
            this._container.RegisterSingleton<IFontCache, FontCache>();
            this._container.RegisterAggregate<IPlatformTargetCreator, TextPlatformTargetCreator>();
            this._container.RegisterAggregate<IPlatformTargetCreator, TexturePlatformTargetCreator>();
            this._container.RegisterAggregate<IPlatformTargetCreator, SpritesheetPlatformTargetCreator>();
            this._container.RegisterAggregate<IPlatformTargetCreator, SolidRectanglePlatformTargetCreator>();
            this._container.RegisterAggregate<IPlatformTargetCreator, BatchTexturePlatformTargetCreator>();
            this._container.Register<ICameraCache, CameraCache>();

            this._platformTargetFactory = this._container.Resolve<IPlatformTargetFactory>();
        }

        public FloatRect GetTextBounds(TextContext textContext, bool forceContextUpdate) {
            if (forceContextUpdate) {
                this._platformTargetFactory.GetPlatformTarget(textContext);
            }
            if (textContext.PlatformTarget is TextPlatformTarget textPlatformTarget) {
                return textPlatformTarget.GetTextBounds();
            }
            throw new InvalidOperationException($"Unable to transform textContext to text platform target");
        }

        public float GetCharacterX(TextContext textContext, int index, bool forceContextUpdate) {

            if (forceContextUpdate) {
                this._platformTargetFactory.GetPlatformTarget(textContext);
            }

            if (textContext.PlatformTarget is TextPlatformTarget platformTarget) {
                return platformTarget.GetCharacterX(index);
            }
            throw new InvalidOperationException($"Unable to transform textContext to text platform target");
        }
    }
}