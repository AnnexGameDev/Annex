using Annex.Core.Data;

namespace Annex.Core.Scenes.Layouts.Html;

public class HtmlSceneVector2f : IVector2<float>
{
    private static readonly char[] KnownCalcOperators = ['-', '+', '/', '*'];
    private readonly string _stringValue;

    public float X { get; private set; }
    public float Y { get; private set; }

    public HtmlSceneVector2f(string stringValue, IVector2<float>? valueToApplyPercentageTo = null, IVector2<float>? offsetToApply = null)
    {
        _stringValue = stringValue;
        Refresh(valueToApplyPercentageTo, offsetToApply);
    }

    public void Refresh(IVector2<float>? valueToApplyPercentageTo = null, IVector2<float>? offsetToApply = null)
    {
        var data = _stringValue.Split(',').Select(val => val.Trim()).ToArray();
        string x = data[0];
        string y = data[1];

        X = ComputeVectorValue(x, valueToApplyPercentageTo?.X ?? 0) + (offsetToApply?.X ?? 0);
        Y = ComputeVectorValue(y, valueToApplyPercentageTo?.Y ?? 0) + (offsetToApply?.Y ?? 0);
    }

    private static float ComputeVectorValue(string val, float parentVal)
    {
        val = val.Trim();

        if (val.StartsWith("calc(") && val.EndsWith(")"))
        {
            val = val[5..^1];
        }

        return Calc.Compute(val, parentVal);
    }

    public void Set(IVector2<float> vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    public void Set(float x, float y)
    {
        X = x;
        Y = y;
    }
}