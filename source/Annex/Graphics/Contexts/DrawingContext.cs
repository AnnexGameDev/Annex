namespace Annex.Graphics.Contexts
{
    public abstract class DrawingContext
    {
        public bool UseUIView { get; set; }

        public DrawingContext() {
            this.UseUIView = false;
        }
    }
}
