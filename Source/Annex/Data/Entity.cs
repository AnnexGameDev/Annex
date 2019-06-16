using Annex.Data.Shared;

namespace Annex.Data
{
    public class Entity
    {
        public readonly Vector EntityPosition;
        public readonly Vector EntitySize;

        public Entity() {
            this.EntityPosition = new Vector();
            this.EntitySize = new Vector();
        }
    }
}
