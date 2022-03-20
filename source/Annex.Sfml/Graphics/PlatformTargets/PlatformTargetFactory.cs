using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal class PlatformTargetFactory : IPlatformTargetFactory
    {
        private readonly ITextureCache _textureCache;

        public PlatformTargetFactory(ITextureCache textureCache) {
            this._textureCache = textureCache;
        }

        public PlatformTarget GetPlatformTarget(DrawContext context) {
            return context switch {
                TextureContext textureContext => GetPlatformTarget<TexturePlatformTarget>(textureContext) ?? new TexturePlatformTarget(textureContext, this._textureCache),
                SpritesheetContext spritesheetContext => GetPlatformTarget<SpritesheetPlatformTarget>(spritesheetContext) ?? new SpritesheetPlatformTarget(spritesheetContext, this._textureCache),
                _ => throw new InvalidOperationException($"Unhandled case: {context.GetType()}")
            };
        }

        private T? GetPlatformTarget<T>(DrawContext context) where T : PlatformTarget {
            if (context.PlatformTarget is T tPlatformTarget) {
                return tPlatformTarget;
            }
            return null;
        }
    }
}
