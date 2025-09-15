using Scaffold.Logging;
using SFML.Graphics;
using SFML.Graphics.Glsl;
using AnnexShader = Annex.Core.Graphics.Shader;

namespace Annex.Sfml.Extensions;

public static class ShaderExtensions
{
    public static void UpdateUniforms(this Shader shader, AnnexShader annexShader)
    {
        try
        {
            if (annexShader.UsesScreenTexture && annexShader.ScreenTexture is Texture screenTexture)
            {
                shader.SetUniform("screen_texture", screenTexture);
            }
            if (annexShader.UsesTexture && annexShader.Texture is Texture texture)
            {
                shader.SetUniform("texture", texture);
            }
            if (annexShader.UsesTime)
            {
                shader.SetUniform("time", annexShader.Time);
            }
            if (annexShader.UsesScreenTextureSize)
            {
                shader.SetUniform("screen_texture_size", new Vec2(annexShader.BufferWidth, annexShader.BufferHeight));
            }
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
        }
    }
}
