using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal class PlatformTargetFactory : IPlatformTargetFactory
    {
        private readonly IEnumerable<IPlatformTargetCreator> _sfmlPlatformTargetCreators;

        public PlatformTargetFactory(IEnumerable<IPlatformTargetCreator> sfmlPlatformTargetCreators) {
            this._sfmlPlatformTargetCreators = sfmlPlatformTargetCreators;
        }

        public PlatformTarget? GetPlatformTarget(DrawContext context) {
            foreach (var creator in this._sfmlPlatformTargetCreators) {
                if (creator.TryGetOrCreate(context, out var sfmlPlatformTarget)) {
                    return sfmlPlatformTarget;
                }
            }
            throw new InvalidOperationException($"Unable to get sfml platform target for {context}");
        }
    }
}
