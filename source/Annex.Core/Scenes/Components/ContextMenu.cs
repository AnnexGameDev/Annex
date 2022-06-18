using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Components
{
    public class ContextMenu : Container, IParentElement
    {
        private readonly SolidRectangleContext _background;

        public ContextMenu(IVector2<float> position, params Item[] contextMenuItems) : base(position: position) {
            this._background = new SolidRectangleContext(KnownColor.White, this.Position, this.Size) {
                BorderColor = KnownColor.Black,
                BorderThickness = 1,
                Camera = CameraId.UI.ToString()
            };

            // We need the widths to be consistent throughout
            float maxWidth = contextMenuItems.Max(item => item.Size.X);
            float totalHeight = contextMenuItems.Sum(item => item.Size.Y);
            this.Size.Set(maxWidth, totalHeight);

            float heightSoFar = 0;
            for (int i = 0; i < contextMenuItems.Length; i++) {
                var child = contextMenuItems[i];

                // Manually set the position
                child.Position.Set(this.Position.X, this.Position.Y + heightSoFar);
                this.AddChild(child);

                heightSoFar += child.Size.Y;
            }
        }

        protected override void DrawInternal(ICanvas canvas) {
            if (this.Visible) {
                canvas.Draw(this._background);
                base.DrawInternal(canvas);
            }
        }

        public class Item : Label
        {
            private bool IsHovered;
            private readonly SolidRectangleContext _hoveredBackground;
            private readonly Action _selectedAction;

            public Item(string text, Action selectionAction) {
                this.Text = text;
                this.Size.Set(75, 30);
                this.FontSize = 18;
                this.TextPositionOffset = new Vector2f(5, this.Size.Y / 2);

                this._hoveredBackground = new SolidRectangleContext(KnownColor.Teal, this.Position, this.Size) {
                    Camera = CameraId.UI.ToString()
                };

                this.HorizontalTextAlignment = HorizontalAlignment.Left;
                this.VerticalTextAlignment = VerticalAlignment.Middle;

                this._selectedAction = selectionAction;
            }

            public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent) {
                base.OnMouseMoved(mouseMovedEvent);
                this.IsHovered = true;
            }

            public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent) {
                base.OnMouseLeft(mouseMovedEvent);
                this.IsHovered = false;
            }

            protected override void DrawInternal(ICanvas canvas) {
                if (this.IsHovered) {
                    canvas.Draw(this._hoveredBackground);
                }
                base.DrawInternal(canvas);
            }

            public override void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent) {
                base.OnMouseButtonPressed(mouseButtonPressedEvent);
                this._selectedAction.Invoke();
            }
        }
    }

}
