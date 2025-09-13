using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using Vector2f = SFML.System.Vector2f;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class BatchTexturePlatformTarget : PlatformTarget
{
    private readonly ITextureCache _textureCache;
    private readonly DrawableVertexArray _drawable;

    public BatchTexturePlatformTarget(BatchTextureContext drawContext, ITextureCache textureCache)
    {
        _textureCache = textureCache;

        var texture = _textureCache.GetTexture(drawContext.TextureId);
        _drawable = new DrawableVertexArray(texture, drawContext);
    }

    public override void Dispose()
    {
        _drawable.Dispose();
    }

    protected override void Draw(RenderTarget renderTarget)
    {
        _drawable.Update();
        renderTarget.Draw(_drawable);
    }

    private class DrawableVertexArray : Transformable, Drawable
    {
        private readonly Texture _texture;
        private readonly VertexArray _vertexArray;
        private readonly BatchTextureContext _drawContext;
        private readonly uint _batchSize;

        public DrawableVertexArray(Texture texture, BatchTextureContext drawContext)
        {
            _texture = texture;
            _batchSize = (uint)drawContext.Positions.Length;
            _drawContext = drawContext;
            _vertexArray = new VertexArray(PrimitiveType.Quads, 4 * _batchSize);

            Update();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            // Calculating all this stuff is expensive
            if (_drawContext.UpdateFrequency != Updatability.NeverUpdates)
            {
                Update();
            }

            states.Transform *= Transform;
            states.Texture = _texture;
            states.Shader = ShaderCache.GetShader(_drawContext.Shader);
            target.Draw(_vertexArray, states);
        }

        private Vector2f[] UpdateRect((int top, int left, int width, int height)? rect)
        {
            if (rect == null)
            {
                return new[] {
                            new Vector2f(0, 0),
                            new Vector2f(_texture.Size.X, 0),
                            new Vector2f(_texture.Size.X, _texture.Size.Y),
                            new Vector2f(0, _texture.Size.Y)
                    };
            }
            else
            {
                var r = rect.Value;
                return new[] {
                            new Vector2f(r.left, r.top),
                            new Vector2f(r.left + r.width, r.top),
                            new Vector2f(r.left + r.width, r.top + r.height),
                            new Vector2f(r.left, r.top + r.height)
                        };
            }
        }

        public void Update()
        {
            for (int i = 0; i < _batchSize; i++)
            {
                uint quadNum = (uint)i * 4;

                var color = _drawContext.GetColor(i).ToSFML(KnownColor.White);
                var rects = UpdateRect(_drawContext.GetSourceTextureRect(i));

                var size = _drawContext.GetSize(i) ?? (_texture.Size.X, _texture.Size.Y);
                var position = _drawContext.GetPosition(i);
                var offset = _drawContext.GetOffset(i) ?? (0, 0);
                var rotation = _drawContext.GetRotation(i) ?? 0;

                float left = position.x + offset.x;
                float top = position.y + offset.y;
                float right = left + size.x;
                float bottom = top + size.y;

                float originx = position.x;
                float originy = position.y;

                (float topleft_x, float topleft_y) = Rotate(left, top, rotation, originx, originy);
                (float bottomright_x, float bottomright_y) = Rotate(right, bottom, rotation, originx, originy);
                (float topright_x, float topright_y) = Rotate(right, top, rotation, originx, originy);
                (float bottomleft_x, float bottomleft_y) = Rotate(left, bottom, rotation, originx, originy);

                _vertexArray[quadNum] = new Vertex(new Vector2f(topleft_x, topleft_y), color, rects[0]);
                _vertexArray[quadNum + 1] = new Vertex(new Vector2f(topright_x, topright_y), color, rects[1]);
                _vertexArray[quadNum + 2] = new Vertex(new Vector2f(bottomright_x, bottomright_y), color, rects[2]);
                _vertexArray[quadNum + 3] = new Vertex(new Vector2f(bottomleft_x, bottomleft_y), color, rects[3]);
            }
        }

        private (float x, float y) Rotate(float x, float y, float rotation, float originx, float originy)
        {
            if (rotation == 0)
            {
                return (x, y);
            }
            return Core.Calculations.Rotation.RotateAbout(x, y, rotation, originx, originy);
        }

        protected override void Destroy(bool disposing)
        {
            base.Destroy(disposing);

            if (disposing)
            {
                _texture.Dispose();
                _vertexArray.Dispose();
                // _drawContext isn't owned by us.
            }
        }
    }
}
