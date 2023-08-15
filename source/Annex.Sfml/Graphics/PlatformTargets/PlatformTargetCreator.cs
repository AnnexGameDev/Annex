using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    public abstract class PlatformTargetCreator<TPlatformTarget> : IPlatformTargetCreator where TPlatformTarget : PlatformTarget
    {
        public bool TryGetOrCreate(DrawContext drawContext, out PlatformTarget? platformTarget) {

            platformTarget = default;

            if (!this.Supports(drawContext))
                return false;

            if (this.GetExistingPlatformTarget(drawContext) is TPlatformTarget existingPlatformTarget) {
                platformTarget = existingPlatformTarget;
                return true;
            }

            var newPlatformTarget = this.CreatePlatformTargetFor(drawContext);
            drawContext.SetPlatformTarget(newPlatformTarget);
            platformTarget = newPlatformTarget;
            return true;
        }

        protected abstract PlatformTarget CreatePlatformTargetFor(DrawContext drawContext);
        protected abstract bool Supports(DrawContext drawContext);

        private TPlatformTarget? GetExistingPlatformTarget(DrawContext context) {
            if (context.PlatformTarget is TPlatformTarget platformTarget) {
                return platformTarget;
            }
            return null;
        }
    }
}
