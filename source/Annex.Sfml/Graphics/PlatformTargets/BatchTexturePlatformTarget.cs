using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using Vector2f = SFML.System.Vector2f;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal class BatchTexturePlatformTarget : PlatformTarget
    {
        private readonly ITextureCache _textureCache;
        private readonly DrawableVertexArray _drawable;

        public BatchTexturePlatformTarget(BatchTextureContext drawContext, ITextureCache textureCache) {
            this._textureCache = textureCache;

            var texture = this._textureCache.GetTexture(drawContext.TextureId);
            this._drawable = new DrawableVertexArray(texture, drawContext);
        }

        public override void Dispose() {
            this._drawable.Dispose();
        }

        protected override void Draw(RenderTarget renderTarget) {
            this._drawable.Update();
            renderTarget.Draw(this._drawable);
        }

        private class DrawableVertexArray : Transformable, Drawable
        {
            private readonly Texture _texture;
            private readonly VertexArray _vertexArray;
            private readonly BatchTextureContext _drawContext;
            private readonly uint _batchSize;

            public DrawableVertexArray(Texture texture, BatchTextureContext drawContext) {
                this._texture = texture;
                this._batchSize = (uint)drawContext.Positions.Length;
                this._drawContext = drawContext;
                this._vertexArray = new VertexArray(PrimitiveType.Quads, 4 * this._batchSize);

                this.Update();
            }

            public void Draw(RenderTarget target, RenderStates states) {

                // Calculating all this stuff is expensive
                if (this._drawContext.UpdateFrequency != Updatability.NeverUpdates) {
                    this.Update();
                }

                states.Transform *= this.Transform;
                states.Texture = this._texture;
                target.Draw(this._vertexArray, states);
            }

            private Vector2f[] UpdateRect((int top, int left, int width, int height)? rect) {
                if (rect == null) {
                    return new[] {
                                new Vector2f(0, 0),
                                new Vector2f(this._texture.Size.X, 0),
                                new Vector2f(this._texture.Size.X, this._texture.Size.Y),
                                new Vector2f(0, this._texture.Size.Y)
                        };
                } else {
                    var r = rect.Value;
                    return new[] {
                                new Vector2f(r.left, r.top),
                                new Vector2f(r.left + r.width, r.top),
                                new Vector2f(r.left + r.width, r.top + r.height),
                                new Vector2f(r.left, r.top + r.height)
                            };
                }
            }

            public void Update() {
                for (int i = 0; i < this._batchSize; i++) {
                    uint quadNum = (uint)i * 4;

                    var color = this._drawContext.GetColor(i).ToSFML(KnownColor.White);
                    var rects = UpdateRect(this._drawContext.GetSourceTextureRect(i));

                    var size = this._drawContext.GetSize(i) ?? (this._texture.Size.X, this._texture.Size.Y);
                    var position = this._drawContext.GetPosition(i);
                    var offset = this._drawContext.GetOffset(i) ?? (0, 0);
                    var rotation = this._drawContext.GetRotation(i) ?? 0;

                    float left = position.x + offset.x;
                    float top = position.y + offset.y;
                    float right = left + size.x;
                    float bottom = top + size.y;

                    float originx = position.x;
                    float originy = position.y;

                    (float topleft_x, float topleft_y) = Core.Calculations.Rotation.RotateAbout(left, top, rotation, originx, originy);
                    (float bottomright_x, float bottomright_y) = Core.Calculations.Rotation.RotateAbout(right, bottom, rotation, originx, originy);
                    (float topright_x, float topright_y) = Core.Calculations.Rotation.RotateAbout(right, top, rotation, originx, originy);
                    (float bottomleft_x, float bottomleft_y) = Core.Calculations.Rotation.RotateAbout(left, bottom, rotation, originx, originy);

                    this._vertexArray[quadNum] = new Vertex(new Vector2f(topleft_x, topleft_y), color, rects[0]);
                    this._vertexArray[quadNum + 1] = new Vertex(new Vector2f(topright_x, topright_y), color, rects[1]);
                    this._vertexArray[quadNum + 2] = new Vertex(new Vector2f(bottomright_x, bottomright_y), color, rects[2]);
                    this._vertexArray[quadNum + 3] = new Vertex(new Vector2f(bottomleft_x, bottomleft_y), color, rects[3]);
                }
            }

            protected override void Destroy(bool disposing) {
                base.Destroy(disposing);

                if (disposing) {
                    this._texture.Dispose();
                    this._vertexArray.Dispose();
                    // _drawContext isn't owned by us.
                }
            }
        }
    }
}
