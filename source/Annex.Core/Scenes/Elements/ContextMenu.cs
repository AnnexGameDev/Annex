using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class ContextMenu : Container, IParentElement
{
    private readonly SolidRectangleContext _background;
    private IScene? _currentScene;

    public ContextMenu(IVector2<float> position, params Item[] contextMenuItems) : base(new (position: position))
    {
        _background = new SolidRectangleContext(KnownColor.White, Position, Size)
        {
            BorderColor = KnownColor.Black,
            BorderThickness = 1.0f,
            Camera = KnownCamera.UI
        };

        // We need the widths to be consistent throughout
        float maxWidth = contextMenuItems.Max(item => item.Size.X);
        float totalHeight = contextMenuItems.Sum(item => item.Size.Y);
        Size.Set(maxWidth, totalHeight);

        float heightSoFar = 0;
        for (int i = 0; i < contextMenuItems.Length; i++)
        {
            var child = contextMenuItems[i];

            // Manually set the position
            child.Position.Set(Position.X, Position.Y + heightSoFar);
            AddChild(child);

            heightSoFar += child.Size.Y;
        }
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        if (Visible)
        {
            window.Draw(_background);
            base.DrawInternal(window, timeDelta);
        }
    }

    public void AddToScene(IScene scene)
    {
        Assert.IsNull(_currentScene);
        _currentScene = scene;
        scene.AddChild(this);
    }

    public void RemoveFromCurrentScene()
    {
        _currentScene!.RemoveChild(this);
        _currentScene = null;
    }

    public class Item : Label
    {
        private bool IsHovered;
        private readonly SolidRectangleContext _hoveredBackground;
        private readonly Action<WindowEvent> _selectedAction;

        public Item(string text, Action<WindowEvent> selectionAction) : base(null)
        {
            Text = text;
            Size.Set(75, 30);
            FontSize = 18;
            TextPositionOffset = new Vector2f(5, Size.Y / 2);

            _hoveredBackground = new SolidRectangleContext(KnownColor.Teal, Position, Size)
            {
                Camera = KnownCamera.UI
            };

            HorizontalTextAlignment = HorizontalAlignment.Left;
            VerticalTextAlignment = VerticalAlignment.Middle;

            _selectedAction = selectionAction;
        }

        public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent)
        {
            base.OnMouseMoved(mouseMovedEvent);
            IsHovered = true;
        }

        public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent)
        {
            base.OnMouseLeft(mouseMovedEvent);
            IsHovered = false;
        }

        protected override void DrawInternal(IWindow window, long timeDelta)
        {
            if (IsHovered)
            {
                window.Draw(_hoveredBackground);
            }
            base.DrawInternal(window, timeDelta);
        }

        public override void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent)
        {
            base.OnMouseButtonPressed(mouseButtonPressedEvent);
            _selectedAction.Invoke(mouseButtonPressedEvent);
        }
    }
}

