using Annex.Data.Shared;

namespace Annex.Data
{
    public class Entity
    {
        public readonly Vector EntityPosition;
        public readonly Vector EntitySize;

        public Entity() : this(new Vector(), new Vector()) {
        }

        public Entity(Vector position, Vector size) {
            this.EntityPosition = position;
            this.EntitySize = size;
        }
    }
}
