namespace Annex.Data
{
    public class Entity
    {
        public readonly Vector2f EntityPosition;
        public readonly Vector2f EntitySize;
        public Vector2f Centerpoint => new Vector2f(this.EntitySize.X / 2, this.EntitySize.Y / 2);

        public Entity() {
            this.EntityPosition = new Vector2f();
            this.EntitySize = new Vector2f();
        }
    }
}
