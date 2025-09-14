using Annex.Core.Graphics;
using Annex.Sfml.Extensions;
using Scaffold.Logging;
using SfmlShader = SFML.Graphics.Shader;

namespace Annex.Sfml.Graphics;

internal static class ShaderCache
{
    private static Dictionary<Shader, SfmlShader> _shaderCache = new();

    internal static SfmlShader? GetShader(Shader? shader)
    {
        if (shader == null || !SfmlShader.IsAvailable)
        {
            return null;
        }

        if (_shaderCache.TryGetValue(shader, out var sfmlShader))
        {
            sfmlShader.UpdateUniforms(shader);
            return sfmlShader;
        }

        try
        {
            _shaderCache.Add(shader, SfmlShader.FromString(shader.VertexShader, shader.GeometryShader, shader.FragmentShader));
            return GetShader(shader);
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
        }
        return null;
    }
}
