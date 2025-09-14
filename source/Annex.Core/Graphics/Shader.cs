
namespace Annex.Core.Graphics;

public class Shader
{
    public string VertexShader;
    public string? GeometryShader;
    public string FragmentShader;

    public bool UsesScreenSize = false;
    public bool UsesScreenTexture = false;
    public bool UsesTexture = false;
    public bool UsesTime = false;

    public object? ScreenSize = null;
    public object? ScreenTexture = null;
    public object? Texture = null;
    public float Time = 0;

    protected const string DefaultVertexShader =
@"void main()
{
    gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
    gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
    gl_FrontColor = gl_Color;
}";

    protected const string DefaultFragmentShader =
@"uniform sampler2D texture;

void main()
{
    gl_FragColor = texture2D(texture, gl_TexCoord[0].xy);
}";

    public Shader(string vertexShader, string? geometryShader, string fragmentShader)
    {
        VertexShader = vertexShader;
        GeometryShader = geometryShader;
        FragmentShader = fragmentShader;
    }

    public Shader(string vertexShader = DefaultVertexShader, string fragmentShader = DefaultFragmentShader) : this(vertexShader, null, fragmentShader)
    {
    }

    public void UpdateUniforms(object? screenTexture, object? texture, float time)
    {
        if (UsesScreenTexture)
        {
            ScreenTexture = screenTexture;
        }
        if (UsesTexture)
        {
            Texture = texture;
        }
        if (UsesTime)
        {
            Time = time;
        }
    }
}
