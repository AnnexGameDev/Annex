using Annex.Data.Binding;

namespace Annex.Data
{
    public class Entity
    {
        public readonly PVector EntityPosition;
        public readonly PVector EntitySize;

        public Entity() {
            this.EntityPosition = new PVector();
            this.EntitySize = new PVector();
        }
    }
}
