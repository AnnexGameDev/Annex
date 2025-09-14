using Scaffold.Logging;
using SFML.Graphics;
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
                shader.SetUniform("base_texture", texture);
            }
            if (annexShader.UsesTime)
            {
                shader.SetUniform("time", annexShader.Time);
            }
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
        }
    }
}
